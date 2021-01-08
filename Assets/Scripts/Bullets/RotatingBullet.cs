using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class RotatingBullet : MonoBehaviour
{
    Bullet bullet;
    [Range(0.1f,600)]
    public float period;

	Quaternion originalRotation;

    public bool reverse;
    [Range(0f,1f)]
    public float forwardMomentum;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
	    StartCoroutine(GetRotationAfterOneFrame());
    }

    void Update()
    {
	    float toRotate = reverse ? -Time.deltaTime * 360 / period : Time.deltaTime * 360 / period;
	    bullet.Rotate(toRotate);
	    bullet.transform.Translate(originalRotation * Vector3.up * (bullet.Speed * forwardMomentum * Time.deltaTime),Space.World);
    }

    IEnumerator GetRotationAfterOneFrame()
    {
	    yield return null;
	    originalRotation = transform.rotation;
    }
}
