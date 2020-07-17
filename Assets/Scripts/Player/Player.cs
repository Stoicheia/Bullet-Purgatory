﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamageable
{
    public delegate void DieAction();
    public static event DieAction OnPlayerHit;
    public static event DieAction OnPlayerDeath;
    public static event DieAction OnLivesChange;

	Controller controller;
    Collider2D hitbox;
    public Animator animator;

    RhythmicObject shooter;

    Vector3 inputVector;
	Vector3 moveVector;
	Vector3 targetMoveVector;
	public float defSpeed;
	float speedMultplier;
	float speed;

    float rotationFactor = 0.05f;
    float rotationSmoothing = 10;
    Quaternion targetShooterRotation;

    [SerializeField]
	private float moveSmoothing = 50;
	public float sprintMod;
	int moveState;

    bool invulnerable;
    float invulnTimeLeft;
    public float invulnerabilityPeriod = 2f;

    public int maxLives = 3;
    int lives;
    public int Lives {get{return lives;} private set{lives = value;}}

    [SerializeField]
    private bool godMode = true;

	//public RhythmicObject startingShooter;
    public RhythmicObject[] shooters;

    void Awake(){
        lives = maxLives;

        controller = GetComponent<Controller>();
        hitbox = GetComponent<Collider2D>();
    }

    void Start(){
    	moveVector = targetMoveVector = new Vector3(0,0,0);
    	speed = defSpeed;
        moveState = 0;

        invulnerable = false;
    }

    void Update()
    {   
        GetMoveInputs();
        UpdateSpeedMultiplier();
        CalculateMoveVector(inputVector);
        UpdateShooterRotation();
        UpdateInvulnerability();
        UpdateAnimatorParams();

        controller.Move(moveVector);
    }

    void GetMoveInputs()
    {
        Vector2 inputAxis = Keybinds.instance.GetInputAxis();
        inputVector = new Vector3(inputAxis.x, inputAxis.y, 0).normalized;
        UpdateMoveState();
    }

    void CalculateMoveVector(Vector3 inVec)
    {
        targetMoveVector = inVec * speed * Time.deltaTime;
        moveVector = Vector3.Lerp(moveVector, targetMoveVector, moveSmoothing*Time.deltaTime);
    }

    void UpdateShooterRotation()
    {
        foreach(RhythmicObject shooter in shooters){
            targetShooterRotation = Quaternion.Euler(new Vector3(0,0,-180/Mathf.PI*Mathf.Atan(moveVector.x/Mathf.Max(0.001f,Time.deltaTime)*rotationFactor))); 
            shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, targetShooterRotation, rotationSmoothing*Time.deltaTime);
        }
    }

    void UpdateAnimatorParams()
    {
        animator.SetFloat("horzSpeed", inputVector.x);
        animator.SetBool("invulnerable", invulnerable);
    }

    void UpdateInvulnerability()
    {
        invulnTimeLeft -= Time.deltaTime;
        invulnerable = invulnTimeLeft > 0;
        hitbox.enabled = !invulnerable;
    }

    void UpdateMoveState(){
    	moveState = 0;
    	if(Keybinds.instance.GetInput("Sprint"))
    		moveState++;
    	if(Keybinds.instance.GetInput("Strafe"))
    		moveState--;
    }

    void UpdateSpeedMultiplier(){
        speedMultplier = Mathf.Pow(sprintMod, moveState);
    	speed = defSpeed * speedMultplier;
    }

    public void TakeHit(float damage, Collision2D col){
        invulnTimeLeft = invulnerabilityPeriod;
        if(!invulnerable)
            GetHit();
        if(lives<=0&&!godMode)
            GetKilled();
        UpdateInvulnerability();
    }

    void GetHit(){
        ChangeLives(-1);
        animator.SetTrigger("onHit");
        if(OnPlayerHit!=null)
            OnPlayerHit();
    }

    void GetKilled(){
        animator.SetTrigger("onDie");
        foreach(RhythmicObject shooter in shooters){
            shooter.gameObject.SetActive(false);
        }
        StartCoroutine(DisableAfterSeconds(Mathf.Max(invulnerabilityPeriod-Time.deltaTime,0)));
        if(OnPlayerDeath!=null)
            OnPlayerDeath();
    }

    IEnumerator DisableAfterSeconds(float s){
        yield return new WaitForSeconds(s);
        gameObject.SetActive(false);
    }

    void ChangeLives(int lv){
        lives=Mathf.Max(lives+lv,0);
        if(OnLivesChange!=null)
            OnLivesChange();
    }

    public bool IsFriendly(){
        return true;
    }

    public int GetMoveState(){
        return moveState;
    }
}
