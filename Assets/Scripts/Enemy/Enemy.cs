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

    public delegate void LocalHitAction();
    public LocalHitAction OnThisDeath;
    public LocalHitAction OnThisHit;

    public string enemyTag;
	public float maxHP;
	float hp;
    public float HP{get{return hp;} private set{hp=value;}}
	public bool shooting;
	RhythmicObject[] myShooters;

    bool dead;

	Bounds myBounds;
	Bounds stageBounds;
	Animator animator;

    AnimatorStateInfo spawnState;

    EnemyAnimations spriteAnimations;

    void OnEnable()
    {
        OnThisDeath += InvokeDeathEvent;
        OnThisHit += InvokeHitEvent;
    }

    void OnDisable()
    {
        OnThisDeath -= InvokeDeathEvent;
        OnThisHit -= InvokeHitEvent;
    }

    void InvokeDeathEvent(){
        if(OnDeath!=null) OnDeath(enemyTag);
    }

    void InvokeHitEvent(){
        if(OnHit!=null && !dead) OnHit(enemyTag);
    }

    void Awake()
    {
        hp = maxHP;
    }
    void Start()
    {
        dead = false;
    	

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
        if (dead) return;
        if(OnThisHit!=null)
            OnThisHit();
        if(hp<=0){
            Die();
            return;
        }
        if(spriteAnimations!=null)
            spriteAnimations.PlayHit();
    }

    public void Die(){
        if(dead) return;
        dead = true;
    	if(OnThisDeath!=null)
    		OnThisDeath();
        StartCoroutine(DeathSequence());
    }

    public void Leave(){
        if(dead) return;
    	if(OnThisDeath!=null)
    		OnThisDeath();
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

    public bool IsSolid(){
        return true;
    }

    public bool IsSpawning(){
        return spawnState.IsName("Spawning");
    }
}
