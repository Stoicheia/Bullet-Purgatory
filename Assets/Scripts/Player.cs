using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller))]
public class Player : MonoBehaviour
{
	Controller controller;

	Vector3 moveVector;
	Vector3 targetMoveVector;
	public float defSpeed;
	float speedMultplier;
	float speed;
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

        RhythmicObject shooter = Instantiate(startingShooter,transform.position,transform.rotation) as RhythmicObject;
        shooter.transform.parent = transform;
    }

    void Update()
    {
        Vector2 inputVector = new Vector3(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"),0).normalized;
        UpdateMoveState(sprintButton, crawlButton);
        speedMultplier = Mathf.Pow(sprintMod, moveState);


        UpdateSpeed();
        targetMoveVector = inputVector * speed * Time.deltaTime;
        moveVector = Vector3.Lerp(moveVector, targetMoveVector, moveSmoothing*Time.deltaTime);
        controller.Move(moveVector);
    }

    void UpdateMoveState(KeyCode a, KeyCode b){
    	moveState = 0;
    	if(Input.GetKey(a))
    		moveState++;
    	if(Input.GetKey(b))
    		moveState--;
    }

    void UpdateSpeed(){
    	speed = defSpeed * speedMultplier;
    }
}
