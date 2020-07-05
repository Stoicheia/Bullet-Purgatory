using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    public delegate void DieAction(string tag);
    public static event DieAction OnDeath;

    public string enemyTag;
	public float maxHP;
	float hp;
	public RhythmicObject shooter;
	public bool shooting;
	RhythmicObject myShooter;

    void Start()
    {
    	hp = maxHP;
    	myShooter = Instantiate(shooter,transform.position,transform.rotation) as RhythmicObject;
        myShooter.transform.parent = transform;   
    }

    void Update()
    {
        myShooter.shootEnabled = shooting;
    }

    public void TakeHit(float damage, Collision2D col){
        hp -= damage;
        if(hp<=0)
        	Die();
    }

    public void Die(){
    	if(OnDeath!=null)
    		OnDeath(enemyTag);
    	Destroy(gameObject);
    }

    public bool IsFriendly(){
        return false;
    }
}
