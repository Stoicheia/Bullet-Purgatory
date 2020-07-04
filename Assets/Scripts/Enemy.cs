using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
	public RhythmicObject shooter;

    void Start()
    {   
    	RhythmicObject myShooter = Instantiate(shooter,transform.position,transform.rotation) as RhythmicObject;
        myShooter.transform.parent = transform;   
    }

    void Update()
    {
        
    }

    public void TakeHit(float damage, Collision2D col){
        
    }

    public bool IsFriendly(){
        return false;
    }
}
