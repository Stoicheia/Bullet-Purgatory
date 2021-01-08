using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class BulletHitParticles : MonoBehaviour
{
    private Bullet myBullet;
    public BulletParticles particles;
    

    private void Awake()
    {
        myBullet = GetComponent<Bullet>();
    }

    private void OnEnable()
    {
        myBullet.OnBulletHit += SpawnParticles;
    }

    private void OnDisable()
    {
        myBullet.OnBulletHit -= SpawnParticles;
    }

    void SpawnParticles(Transform t)
    {
        BulletParticles myParticles;
        if (particles != null)
        {
            myParticles = Instantiate(particles, t.position, t.rotation);
            myParticles.Play();
            myParticles.DestroyAfterSeconds(myParticles.GetDuration());
        }
    }
}
