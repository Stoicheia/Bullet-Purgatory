using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class DeathBurst : MonoBehaviour
{
	public Bullet bulletType;
	[Range(1,200)]
	public int bulletAmount;
	public float minSpeed;
	public float maxSpeed;
	public int revolutions;
	public bool randomSpread;
	ObjectPooler pooler;
	Enemy enemy;

	void Awake()
	{
		enemy = GetComponent<Enemy>();
	}
 
    void Start()
    {
        pooler = FindObjectOfType<ObjectPooler>();
    }

 	void OnEnable()
 	{
 		enemy.OnThisDeath += Burst;
 	}

 	void OnDisable()
 	{
 		enemy.OnThisDeath -= Burst;
 	}

 	void Burst()
 	{
 		for(int i=0; i<bulletAmount; i++)
 		{
 			Bullet b = pooler.Spawn(bulletType.gameObject, bulletType.poolTag
 				,transform.position, transform.rotation*Quaternion.Euler(0,0,GetAngle(i))).GetComponent<Bullet>();
 			b.SetSpeed(Random.Range(minSpeed,maxSpeed));
 			b.SetFriendly(false);
 		}
 	}

 	float GetAngle(int i)
 	{
 		return randomSpread?Random.Range(0,360):revolutions*360*i/bulletAmount;
 	}
}

