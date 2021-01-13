using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class AcceleratingBullet : MonoBehaviour
{
    Bullet bullet;
    public float accelerateToPercent;
    public bool stopInsteadOfReversing;

    public bool repeatSequence;
    [Range(0.1f,20)]
    public float duration;

    float initialSpeed;
    float targetSpeed;

    float startTime;
    private bool disabled;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
    }

    void OnEnable()
    {
	    Enable();
        startTime = Time.time;
    }

    private void Start()
    {
	    initialSpeed = bullet.Speed;
	    targetSpeed = bullet.Speed*accelerateToPercent/100;
    }

    void Update()
    {
	    if (disabled) return;
	    
	    float toSpeed;
    	if(!repeatSequence)
			toSpeed = targetSpeed*((Time.time-startTime)/duration)+initialSpeed*(1-((Time.time-startTime)/duration));
		else
			toSpeed = initialSpeed+(targetSpeed-initialSpeed)*1/2*(1+Mathf.Sin(2*Mathf.PI*(Time.time-startTime)/duration));
        bullet.Speed = stopInsteadOfReversing ? Mathf.Max(0, toSpeed) : toSpeed;
    }

    public void Disable()
    {
	    disabled = true;
	    bullet.AddSpeed(0.5f);
    }

    public void Enable()
    {
	    disabled = false;
    }
}
