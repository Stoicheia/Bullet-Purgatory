using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public float spawnDelayMs;

    public void Start(){
    	Wait(spawnDelayMs);
    }

    IEnumerator Wait(float waitTime){
    	yield return new WaitForSeconds(waitTime/1000);
    }
}
