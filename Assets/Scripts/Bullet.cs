using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
	float speed;
	Vector3 moveVector;
	bool friendly = false;

	MeshCollider stage;
	SpriteRenderer render;


    void Start()
    {
        stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>();
        render = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
    	moveVector = new Vector3(0,1,0)*speed*Time.deltaTime;
        Move(moveVector);
    }

    void Move(Vector3 v){
    	transform.Translate(v);
    	if(Mathf.Abs(transform.position.x-stage.transform.position.x)>stage.bounds.size.x/2+render.bounds.size.x/2||
    		Mathf.Abs(transform.position.y-stage.transform.position.y)>stage.bounds.size.y/2+render.bounds.size.y/2){
    		Destroy(gameObject);
    	}
    }

    void CheckCollisions(){

    }

    public void SetSpeed(float s){
    	speed = s;
    }

    public void Rotate(float r){
    	transform.rotation *= Quaternion.Euler(0,0,r);
    }

    public void SetFriendly(bool f){
    	friendly = f;
    }
}
