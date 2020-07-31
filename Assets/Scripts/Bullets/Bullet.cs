using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour
{
    public float damage = 1;
    public float speedModifier = 1;

    static float universalSpeedModifier = 1;
    public static float UniversalSpeedModifier{get{return universalSpeedModifier;} set{universalSpeedModifier = value;}}
    static float pauseZoom = 1;
    public static float PauseZoom{get{return pauseZoom;} set{pauseZoom = value;}}

	float speed;
    public float Speed{get{return speed;} set{speed = value;}}
    float actualSpeed;
	Vector3 moveVector;
	bool friendly = false;

	static MeshCollider stage;
    public string poolTag = "";
    static ObjectPooler pooler;
	SpriteRenderer render;

    const float FRIENDLY_TRANSPARENCY = 0.5f;


    void Awake()
    {
        if(poolTag=="")
            poolTag = name;
        render = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    void Update()
    {
        actualSpeed = speed*speedModifier*universalSpeedModifier*pauseZoom;
        float moveDistance = actualSpeed*Time.deltaTime;
    	moveVector = new Vector3(0,1,0)*moveDistance;
        Move(moveVector);
    }

    void Move(Vector3 v){
    	transform.Translate(v);
    	if(Mathf.Abs(transform.position.x-stage.transform.position.x)>stage.bounds.size.x/2+render.bounds.size.x/2||
    		Mathf.Abs(transform.position.y-stage.transform.position.y)>stage.bounds.size.y/2+render.bounds.size.y/2){
    		Despawn();
    	}
    }


    void OnCollisionEnter2D(Collision2D collision){
        OnHit(collision);
        OnOverlapEnter(collision);
    }

    void OnCollisionExit2D(Collision2D collision){
        OnOverlapExit(collision);
    }

    void OnHit(Collision2D hit){
        IDamageable hitObject = hit.gameObject.GetComponent<IDamageable>();
        if(hitObject!=null){
            if(hitObject.IsFriendly()!=friendly){
                hitObject.TakeHit(damage, hit);
                Despawn();
            }
        }
    }

    void OnOverlapEnter(Collision2D hit){
        IOverlap overObject = hit.gameObject.GetComponent<IOverlap>();
        if(overObject!=null){
            if(overObject.IsFriendly()!=friendly){
                overObject.Enter(hit);
            }
        }
    }

    void OnOverlapExit(Collision2D hit){
        IOverlap overObject = hit.gameObject.GetComponent<IOverlap>();
        if(overObject!=null){
            if(overObject.IsFriendly()!=friendly){
                overObject.Exit(hit);
            }
        }
    }

    
    public void AddSpeed(float a){
        speed += a;
    }

    public void MultiplySpeed(float a){
        speed *= a;
    }

    public void Rotate(float r){
    	transform.rotation *= Quaternion.Euler(0,0,r);
    }

    public bool IsFriendly(){
        return friendly;
    }

    public void SetFriendly(bool f){
    	friendly = f;
        gameObject.layer = friendly? 8:9;
        render.sortingLayerName = friendly? "Player_Bullets":"Bullets";
        if(friendly) render.color = new Color(1,1,1,FRIENDLY_TRANSPARENCY);
        else render.color = new Color(1,1,1,1);
    }

    public static void SetStage(MeshCollider s){
        stage = s;
    }

    public static void SetPooler(ObjectPooler p){
        pooler = p;
    }

    public void Despawn(){
        pooler.Despawn(gameObject, poolTag);
    }

}
