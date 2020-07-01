using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RhythmicObject : MonoBehaviour 
{
	public Bullet[] toSpawn;
	protected int currentStyle;

	public float bulletSpeed;

	public bool shootEnabled;
	public bool friendly = false;

    void Start()
    {
    	currentStyle = 0;
        shootEnabled = true;
    }


    void Update()
    {
       
    }

    public abstract void Shoot();
    public virtual void Change(int i){ 
    	currentStyle = i;
    }

    protected Bullet GetBullet(){
    	return toSpawn[currentStyle%toSpawn.Length];
    }
}
