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

	Bounds myBounds;
	Bounds stageBounds;
	Animator animator;

    void Start()
    {
    	hp = maxHP;
    	myShooter = Instantiate(shooter,transform.position,transform.rotation) as RhythmicObject;
        myShooter.transform.parent = transform;   

        myBounds = GetComponent<SpriteRenderer>().bounds;
        stageBounds = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>().bounds;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        myShooter.shootEnabled = shooting;
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Spawning"))
        	CheckOutOfBounds();
    }

    void CheckOutOfBounds(){
		if(transform.position.x-myBounds.size.x/2>stageBounds.max.x
			||transform.position.x/2+myBounds.size.x<stageBounds.min.x
			||transform.position.y-myBounds.size.y/2>stageBounds.max.y
			||transform.position.y/2+myBounds.size.y<stageBounds.min.y)
		{
    		Leave();
    	}
    }

    public void TakeHit(float damage, Collision2D col){
        hp -= damage;
        if(hp<=0)
        	Die();
    }

    public void Die(){
    	if(OnDeath!=null)
    		OnDeath(enemyTag);
        Deactivate();
    }

    public void Leave(){
    	if(OnDeath!=null)
    		OnDeath(enemyTag);
    	Deactivate();
    }

    void Deactivate(){
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public bool IsFriendly(){
        return false;
    }
}
