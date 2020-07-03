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
        pooler = FindObjectOfType(typeof(ObjectPooler)) as ObjectPooler;
    }

    public abstract override void Shoot();

    protected Bullet GetBullet(){
    	return toSpawn[currentStyle%toSpawn.Length];
    }
}