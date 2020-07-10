using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
	public Transform rotationCenter;
	[Range(0.1f,20)]
	public float rotationPeriod;
	public bool reverse;

	float radius;
	float startingAngle;
	float startTime;
	bool orbitStarted;

	Enemy enemy;

    void Awake()
    {
    	orbitStarted = false;
		enemy = GetComponentInChildren<Enemy>();
		Vector2 differenceVector = new Vector2(transform.position.x-rotationCenter.position.x,transform.position.y-rotationCenter.position.y);
		radius = differenceVector.magnitude;
		startingAngle = -Mathf.Atan2(differenceVector.x,differenceVector.y)*180/Mathf.PI;

    }

    void Update()
    {
        if(!enemy.IsSpawning()&&!orbitStarted){
        	orbitStarted = true;
        	startTime = Time.time;
        }
        float t = Time.time-startTime;
        float angle = reverse? startingAngle-t*2*Mathf.PI/rotationPeriod:startingAngle+t*2*Mathf.PI/rotationPeriod;
        Vector3 newPos = new Vector3(radius*Mathf.Sin(angle),radius*Mathf.Cos(angle),0);
        transform.position = newPos;
    }
}
