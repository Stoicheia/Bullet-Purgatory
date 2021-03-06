﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bullet))]
public class EmptyBullet : MonoBehaviour
{
	Bullet bullet;

	void Awake()
	{
		bullet = GetComponent<Bullet>();
	}

    void OnEnable()
    {
    	bullet.Despawn();
    }

}
