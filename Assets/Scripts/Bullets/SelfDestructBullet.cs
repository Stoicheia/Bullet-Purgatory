using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class SelfDestructBullet : MonoBehaviour
{
    Bullet bullet;
    [Range(0.1f,60)]
    public float lifetime;

    void Awake()
    {
    	bullet = GetComponent<Bullet>();
    }

    void Start()
    {
		StartCoroutine(SDSequence(lifetime));       
    }

    IEnumerator SDSequence(float s)
    {
    	yield return new WaitForSeconds(s);
    	bullet.Despawn();
    }
}
