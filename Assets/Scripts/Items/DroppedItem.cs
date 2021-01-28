using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class DroppedItem : MonoBehaviour
{
	private const float MAX_ITEM_SPEED = 12.5f;
	
	public string poolTag;

	public float itemSize = 1;

	ItemPicker magnet;
	Transform attractTo;
	public float speed;
	public float acceleration;
	float targetSpeed;
	float currentSpeed;
	float magnetIntensity;
	float pickupDistance;

	Vector3 attractVector = new Vector3(0,0,0);
	const float RANDOM_SPEED_FACTOR = 1.5f;
	const float RANDOM_POS_FACTOR = 0.5f;

	[SerializeField]
	public Item item;

	public static List<DroppedItem> itemsOnScreen;

    void Awake()
    {
    	pickupDistance = GetComponent<SpriteRenderer>().bounds.size.y/2;
    }

    void OnEnable()
    {
	    if (itemsOnScreen == null) itemsOnScreen = new List<DroppedItem>();
	    itemsOnScreen.Add(this);
	    transform.localScale *= itemSize;
	    magnet = FindObjectOfType<ItemPicker>();
	    if (magnet != null)
	    {
		    magnetIntensity = magnet.magnetStrength;
		    attractTo = magnet.transform;
	    }

	    targetSpeed = speed;
    	currentSpeed = 0;
    	RandomiseSpeed(RANDOM_SPEED_FACTOR);
    	RandomisePosition(RANDOM_POS_FACTOR);
    }

    private void OnDisable()
    {
	    itemsOnScreen.Remove(this);
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
	    if(magnet!=null)
			magnetIntensity = magnet.magnetStrength;
    	currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration*Time.deltaTime);
    	transform.Translate(-Vector3.up*currentSpeed*Time.deltaTime);
    	Vector3 vectorToMagnet = new Vector3(attractTo.position.x-transform.position.x,attractTo.position.y-transform.position.y,0);
    	attractVector += vectorToMagnet*magnetIntensity/Mathf.Pow(vectorToMagnet.magnitude,3);
    	attractVector *= attractVector.magnitude > MAX_ITEM_SPEED ? MAX_ITEM_SPEED/attractVector.magnitude : 1;
    	attractVector *= vectorToMagnet.magnitude > magnetIntensity*5 ? 0 : 1;
        transform.Translate(transform.rotation*attractVector*Time.deltaTime);
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
