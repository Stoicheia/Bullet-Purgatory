using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class RotatingBullet : MonoBehaviour
{
    Bullet bullet;
    [Range(0.1f,10)]
    public float period;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
    }

    void Update()
    {
		bullet.Rotate(Time.deltaTime*360/period);        
    }
}
