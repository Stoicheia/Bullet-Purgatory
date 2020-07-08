using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class EmptyBullet : MonoBehaviour
{
	//Bullet bullet;
	ObjectPooler pooler;

    void Start()
    {
        pooler = FindObjectOfType(typeof(ObjectPooler)) as ObjectPooler;
        //bullet = GetComponent<Bullet>();
        pooler.Despawn(gameObject, "empty");
    }
}
