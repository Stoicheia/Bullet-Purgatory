using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpreadShooter))]
public class ArmRotator : MonoBehaviour
{
	SpreadShooter shooter;

    public float rotateSpeed;
    public float spreadChangeSpeed;

    void Awake(){
    	shooter = GetComponent<SpreadShooter>();
    }

    void Update()
    {
        shooter.Rotate(rotateSpeed, (spreadChangeSpeed+1)*rotateSpeed);
    }
}
