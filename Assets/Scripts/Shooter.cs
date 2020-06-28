using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
	public Bullet bullet;
	public float bulletSpeed;

	public bool shootEnabled;
	bool friendly = false;
	//temp
	public float shootInterval;
	float nextShootTime;

    void Start()
    {
        shootEnabled = true;
    }


    void Update()
    {
        if(enabled&&Time.time>nextShootTime){
        	nextShootTime = Time.time + shootInterval/1000;
        	Shoot();
        }
    }

    protected virtual void Shoot(){
    	Bullet b = Instantiate(bullet, transform.position, transform.rotation) as Bullet;
        b.SetSpeed(bulletSpeed);
        b.friendly = friendly;
    }

    public void SetFriendly(bool f){
    	friendly = f;
    }
}
