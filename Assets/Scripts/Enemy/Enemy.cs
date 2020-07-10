using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour, IDamageable
{
    public delegate void HitAction(string tag);
    public static event HitAction OnDeath;
    public static event HitAction OnHit;

    public string enemyTag;
	public float maxHP;
	float hp;
	public bool shooting;
	RhythmicObject[] myShooters;

    bool dead;

	Bounds myBounds;
	Bounds stageBounds;
	Animator animator;

    AnimatorStateInfo spawnState;

    EnemyAnimations spriteAnimations;

    void Start()
    {
        dead = false;
    	hp = maxHP;

    	myShooters = GetComponentsInChildren<RhythmicObject>();

        myBounds = GetComponent<Collider2D>().bounds;
        stageBounds = GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>().bounds;

        animator = GetComponent<Animator>();
        spriteAnimations = GetComponent<EnemyAnimations>();
    }

    void Update()
    {
        spawnState = animator.GetCurrentAnimatorStateInfo(0);
        foreach(RhythmicObject shooter in myShooters)
            shooter.shootEnabled = shooting;
        if(!spawnState.IsName("Spawning"))
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
        if(OnHit!=null)
            OnHit(enemyTag);
        if(hp<=0){
            Die();
            return;
        }
        if(spriteAnimations!=null)
            spriteAnimations.PlayHit();
    }

    public void Die(){
        if(dead) return;
    	if(OnDeath!=null)
    		OnDeath(enemyTag);
        StartCoroutine(DeathSequence());
    }

    public void Leave(){
        if(dead) return;
    	if(OnDeath!=null)
    		OnDeath(enemyTag);
       Deactivate();
    }

    IEnumerator DeathSequence(){
        dead = true;
        DisableCollisions();
        if(spriteAnimations!=null){
            spriteAnimations.PlayDeath();
            yield return new WaitForSeconds(spriteAnimations.DeathAnimDuration());
        }
        Deactivate();
    }

    void DisableCollisions(){
        GetComponent<Collider2D>().enabled = false;
        foreach(Transform child in transform){
            if(child.GetComponent<RhythmicObject>()!=null){
                child.gameObject.SetActive(false);
            }
        }
    }

    void Deactivate(){
        foreach(Transform child in transform)
            child.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public bool IsFriendly(){
        return false;
    }

    public bool IsSpawning(){
        return spawnState.IsName("Spawning");
    }
}
