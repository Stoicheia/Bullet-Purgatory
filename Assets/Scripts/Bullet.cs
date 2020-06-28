using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Bullet : MonoBehaviour
{
	protected float speed;
	protected float rotation;
	Vector3 moveVector;
	public bool friendly = false;

	MeshCollider stage;
	SpriteRenderer render;

    protected void Start()
    {
        stage = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>();
        render = GetComponent<SpriteRenderer>();
    }


    protected void Update()
    {
    	transform.rotation = Quaternion.Euler(0,0,rotation);
    	moveVector = new Vector3(0,1,0)*speed*Time.deltaTime;
        Move(moveVector);
    }

    void Move(Vector3 v){
    	transform.Translate(v);
    	if(Mathf.Abs(transform.position.x-stage.transform.position.x)>stage.bounds.size.x+render.bounds.size.x||
    		Mathf.Abs(transform.position.y-stage.transform.position.y)>stage.bounds.size.y/2+render.bounds.size.y/2){
    		Destroy(gameObject);
    	}
    }

    public void SetSpeed(float s){
    	speed = s;
    }

    public void Rotate(float r){
    	rotation += r;
    }
}
