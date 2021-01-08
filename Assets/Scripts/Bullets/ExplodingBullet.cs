using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SelfDestructBullet))]
public class ExplodingBullet : MonoBehaviour
{
    private ObjectPooler pooler;
    private Bullet myBullet;
    
    private bool active;
    private SelfDestructBullet sdBullet;
    [SerializeField] private Bullet bulletToShoot;
    [SerializeField] private int numberOfBullets;
    [SerializeField] private bool randomSpread;
    [SerializeField] private float explodedBulletSpeed;

    private void Awake()
    {
        active = true;
        sdBullet = GetComponent<SelfDestructBullet>();
        myBullet = GetComponent<Bullet>();
    }

    private void Start()
    {
        pooler = FindObjectOfType<ObjectPooler>();
    }

    private void OnEnable()
    {
        sdBullet.OnSD += Explode;
    }

    private void OnDisable()
    {
        sdBullet.OnSD -= Explode;
    }

    void Explode()
    {
        if (!active || !myBullet.GetComponent<Collider2D>().enabled) return;
        Vector3 epicenter = transform.position;
        Quaternion zeroRotation = transform.rotation;
        for (int i = 0; i < numberOfBullets; i++)
        {
            Bullet shotBullet;
            if (!randomSpread)
            {
                shotBullet = pooler.Spawn(bulletToShoot.gameObject, bulletToShoot.poolTag, epicenter,
                    zeroRotation * Quaternion.AngleAxis(i * 360 / numberOfBullets, Vector3.forward)).GetComponent<Bullet>();
                shotBullet.Speed = explodedBulletSpeed/2.5f;
                shotBullet.SetFriendly(myBullet.IsFriendly());
            }
            else
            {
                shotBullet = pooler.Spawn(bulletToShoot.gameObject, bulletToShoot.poolTag, epicenter,
                    zeroRotation * Quaternion.AngleAxis(UnityEngine.Random.Range(0, 360.0f), Vector3.forward)).GetComponent<Bullet>();
                shotBullet.Speed = explodedBulletSpeed;
                shotBullet.SetFriendly(myBullet.IsFriendly());
            }

            ExplodingBullet e = shotBullet.GetComponent<ExplodingBullet>();
            if(e!=null) e.Deactivate();
        }
    }

    public void Deactivate()
    {
        active = false;
    }
}
