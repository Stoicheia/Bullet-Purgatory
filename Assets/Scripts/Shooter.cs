using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shooter : MonoBehaviour
{
	ShooterRhythm rhythm;

	public Bullet bullet;
	public float bulletSpeed;

	public bool shootEnabled;
	public bool friendly = false;

    void Start()
    {
        shootEnabled = true;
    }


    void Update()
    {
       
    }

    public abstract void Shoot();
    public virtual void Change(int i){
    	Debug.Log(i);
    }
}
