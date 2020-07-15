using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class SelfDestructBullet : MonoBehaviour
{
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
		StartCoroutine(SDSequence(lifetime, sdTime));       
    }

    IEnumerator SDSequence(float s, float t)
    {
    	yield return new WaitForSeconds(s);
        if(sdAnimator!=null)
            sdAnimator.SetTrigger("onSD");
        col.enabled = false;
        yield return new WaitForSeconds(t);
    	bullet.Despawn();
    }
}
