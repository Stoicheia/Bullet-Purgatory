using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamageable
{
    public delegate void DieAction();
    public static event DieAction OnPlayerHit;

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

	public float moveSmoothing;
	public float sprintMod;
	KeyCode sprintButton = KeyCode.LeftShift;
	KeyCode strafeButton = KeyCode.Space;
	int moveState;

    bool invulnerable;
    float invulnTimeLeft;
    public float invulnerabilityPeriod = 2f;

	public RhythmicObject startingShooter;

    void Start(){
    	controller = GetComponent<Controller>();
        hitbox = GetComponent<Collider2D>();

    	moveVector = targetMoveVector = new Vector3(0,0,0);
    	speed = defSpeed;
        moveState = 0;

        sprintButton = Keybinds.instance.keys["Sprint"];
        strafeButton = Keybinds.instance.keys["Strafe"];

        shooter = Instantiate(startingShooter,transform.position,transform.rotation) as RhythmicObject;
        shooter.transform.parent = transform;

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
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0).normalized;
        UpdateMoveState(sprintButton, strafeButton);
    }

    void CalculateMoveVector(Vector3 inVec)
    {
        targetMoveVector = inVec * speed * Time.deltaTime;
        moveVector = Vector3.Lerp(moveVector, targetMoveVector, moveSmoothing*Time.deltaTime);
    }

    void UpdateShooterRotation()
    {
        targetShooterRotation = Quaternion.Euler(new Vector3(0,0,-180/Mathf.PI*Mathf.Atan(moveVector.x/Time.deltaTime*rotationFactor))); 
        shooter.transform.rotation = Quaternion.Slerp(shooter.transform.rotation, targetShooterRotation, rotationSmoothing*Time.deltaTime);
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

    void UpdateMoveState(KeyCode a, KeyCode b){
    	moveState = 0;
    	if(Input.GetKey(a))
    		moveState++;
    	if(Input.GetKey(b))
    		moveState--;
    }

    void UpdateSpeedMultiplier(){
        /*switch(moveState){
            case -1:
                speedMultplier = 1/sprintMod;
                break;
            case 0:
                speedMultplier = 1;
                break;
            case 1:
                speedMultplier = sprintMod;
                break;
        }*/
        speedMultplier = Mathf.Pow(sprintMod, moveState);
    	speed = defSpeed * speedMultplier;
    }

    public void TakeHit(float damage, Collision2D col){
        invulnTimeLeft = invulnerabilityPeriod;
        if(OnPlayerHit!=null)
            OnPlayerHit();
    }

    public bool IsFriendly(){
        return true;
    }

    public int GetMoveState(){
        return moveState;
    }
}
