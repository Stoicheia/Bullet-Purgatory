using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class SelfDestructBullet : MonoBehaviour
{
    public delegate void DestructAction();

    public event DestructAction OnSD;
    
    Bullet bullet;
    Collider2D col;
    public Animator sdAnimator;
    [Range(0.1f,60)]
    public float lifetime;
    public float sdTime;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
        col = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        sdAnimator.SetTrigger("onReset");
        col.enabled = true;
		StartCoroutine(SDSequence(lifetime, sdTime));
    }

    IEnumerator SDSequence(float s, float t)
    {
	    yield return new WaitForSeconds(s);
        sdAnimator.ResetTrigger("onReset");
        sdAnimator.SetTrigger("onSD");
        bullet.Speed = 0;
        OnSD?.Invoke();
        sdAnimator.speed = 1 / t;
        col.enabled = false;
        yield return new WaitForSeconds(t);
        bullet.Despawn();
    }

    private void OnValidate()
    {
        if (sdTime < 0.01f)
            sdTime = 0.01f;
    }
}
