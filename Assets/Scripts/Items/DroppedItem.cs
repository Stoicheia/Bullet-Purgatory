using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DroppedItem : MonoBehaviour
{
	public string poolTag;

	ItemPicker magnet;
	Transform attractTo;
	public float speed;
	public float acceleration;
	float targetSpeed;
	float currentSpeed;
	public float magnetIntensity;
	float pickupDistance;

	Vector3 attractVector = new Vector3(0,0,0);
	const float RANDOM_SPEED_FACTOR = 1.5f;
	const float RANDOM_POS_FACTOR = 0.5f;

	[SerializeField]
	public Item item;

    void Awake()
    {
    	pickupDistance = GetComponent<SpriteRenderer>().bounds.size.y/2;
    }

    void OnEnable()
    {
    	magnet = FindObjectOfType<ItemPicker>();
    	if(magnet!=null)
    		attractTo = magnet.transform;
    	targetSpeed = speed;
    	currentSpeed = 0;
    	RandomiseSpeed(RANDOM_SPEED_FACTOR);
    	RandomisePosition(RANDOM_POS_FACTOR);
    }

    void Update()
    {
    	if(magnet==null){
    		magnet = FindObjectOfType<ItemPicker>();
    		if(magnet!=null)
    			attractTo = magnet.transform;
    	}
        Move();
        if(CheckCollision()){
        	Destroy(gameObject);
        	item.InvokeEffect(magnet);
        }
        CheckOutOfBounds();
    }

    void Move()
    {
    	currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration*Time.deltaTime);
    	transform.Translate(Vector3.up*currentSpeed*Time.deltaTime);
    	Vector3 vectorToMagnet = new Vector3(attractTo.position.x-transform.position.x,attractTo.position.y-transform.position.y,0);
    	attractVector += Time.deltaTime*vectorToMagnet*magnetIntensity/Mathf.Pow(vectorToMagnet.magnitude,3);
    	attractVector *= attractVector.magnitude > 0.2f ? 0.2f/attractVector.magnitude : 1;
    	attractVector *= vectorToMagnet.magnitude > magnetIntensity*5 ? 0 : 1;
    	transform.Translate(transform.rotation*attractVector);
    }

    bool CheckCollision()
    {
    	if (pickupDistance>=
    		(new Vector2(attractTo.position.x,attractTo.position.y)
    		-new Vector2(transform.position.x,transform.position.y)).magnitude){
    		return true;
    	}

    	return false;
    }

    void CheckOutOfBounds()
    {
    	if(transform.position.y<-20)
    		Destroy(gameObject);
    }

    void RandomiseSpeed(float factor){
    	if(factor==0) {targetSpeed = 0; return;}
    	targetSpeed *= Random.Range(1/factor, factor);
    }

    void RandomisePosition(float factor){
    	transform.Translate(new Vector3(Random.Range(-factor,factor),0,0));
    }

}
