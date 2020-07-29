using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Controller : MonoBehaviour
{
	MeshCollider stage;
	CircleCollider2D myCollider;
	Bounds stageBounds;
	Bounds myBounds;

    void Start(){
        stage = GameObject.FindWithTag("Stage").GetComponent<MeshCollider>();
    	stageBounds = stage.bounds;
    	myCollider = GetComponent<CircleCollider2D>();
    	myBounds = myCollider.bounds;
    }

    public void Move(Vector3 v)
    {
    	myBounds = myCollider.bounds;
    	CheckCollisions(ref v);
    	transform.Translate(v);
    }

    public void CheckCollisions(ref Vector3 v){
    	if(myBounds.max.x+v.x>stageBounds.max.x)
    		v.x = stageBounds.max.x-myBounds.max.x;
    	if(myBounds.min.x+v.x<stageBounds.min.x)
    		v.x = stageBounds.min.x-myBounds.min.x;
    	if(myBounds.max.y+v.y>stageBounds.max.y)
    		v.y = stageBounds.max.y-myBounds.max.y;
    	if(myBounds.min.y+v.y<stageBounds.min.y)
    		v.y = stageBounds.min.y-myBounds.min.y;
    }

}
