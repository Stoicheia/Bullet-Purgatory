using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour, IDamageable
{
	Controller controller;
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
	public KeyCode sprintButton;
	public KeyCode crawlButton;
	int moveState;

	public RhythmicObject startingShooter;

    void Start(){
    	controller = GetComponent<Controller>();

    	moveVector = targetMoveVector = new Vector3(0,0,0);
    	speed = defSpeed;
        moveState = 0;

        shooter = Instantiate(startingShooter,transform.position,transform.rotation) as RhythmicObject;
        shooter.transform.parent = transform;
    }

    void Update()
    {   
        GetMoveInputs();
        UpdateSpeedMultiplier();
        CalculateMoveVector(inputVector);
        UpdateShooterRotation();
        UpdateAnimatorParams();

        controller.Move(moveVector);
    }

    void GetMoveInputs()
    {
        inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0).normalized;
        UpdateMoveState(sprintButton, crawlButton);
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
    }


    void UpdateMoveState(KeyCode a, KeyCode b){
    	moveState = 0;
    	if(Input.GetKey(a))
    		moveState++;
    	if(Input.GetKey(b))
    		moveState--;
    }

    void UpdateSpeedMultiplier(){
        speedMultplier = Mathf.Pow(sprintMod, moveState);
    	speed = defSpeed * speedMultplier;
    }

    public void TakeHit(float damage, Collision2D col){
        
    }

    public bool IsFriendly(){
        return true;
    }
}
