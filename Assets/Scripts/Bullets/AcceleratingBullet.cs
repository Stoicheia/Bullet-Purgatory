using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class AcceleratingBullet : MonoBehaviour
{
    Bullet bullet;
    [Range(0,500)]
    public float accelerateToPercent;

    public bool repeatSequence;
    [Range(0.1f,20)]
    public float duration;

    float initialSpeed;
    float targetSpeed;

    float startTime;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
    }

    void Start()
    {
    	initialSpeed = bullet.Speed;
    	targetSpeed = bullet.Speed*accelerateToPercent/100;
    }

    void OnEnable()
    {
        startTime = Time.time;
    }

    void Update()
    {
    	if(!repeatSequence)
			bullet.Speed = initialSpeed+(targetSpeed-initialSpeed)*(1-1/(1+(Time.time-startTime)/duration));
		else
			bullet.Speed = initialSpeed+(targetSpeed-initialSpeed)*1/2*(1+Mathf.Sin(2*Mathf.PI*(Time.time-startTime)/duration));
    }
}
