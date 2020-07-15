using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : RhythmicObject 
{
	public Bullet[] toSpawn;
	public float bulletSpeed;
	public bool friendly = false;
    protected ObjectPooler pooler;

    void Start()
    {
        pooler = FindObjectOfType<ObjectPooler>();
    }

    public abstract override void Shoot();

    protected Bullet GetBullet(){
    	return toSpawn[currentStyle%toSpawn.Length];
    }

    protected Bullet SpawnFromPoolAtAngle(float angle){
    	Bullet toSpawn = GetBullet();
    	Bullet b = pooler.Spawn(toSpawn.gameObject, toSpawn.poolTag, transform.position, transform.rotation*Quaternion.Euler(0,0,angle)).GetComponent<Bullet>();
        b.Speed = bulletSpeed/2;
       	b.SetFriendly(friendly);  
       	return b;     	
    }
}