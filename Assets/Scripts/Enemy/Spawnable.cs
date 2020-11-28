using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    public float spawnDelayMs;	

    public void Start(){
    	StartCoroutine(Wait(spawnDelayMs));
    }

    IEnumerator Wait(float waitTime){
    	foreach(Transform c in transform){
    		c.gameObject.SetActive(false);
    		Animator animator = c.GetComponent<Animator>();
    		if(animator!=null)
    			animator.enabled = false;
    	}
    	yield return new WaitForSeconds(waitTime/1000);
    	foreach(Transform c in transform){
    		c.gameObject.SetActive(true);
    		Animator animator = c.GetComponent<Animator>();
    		if(animator!=null)
    			animator.enabled = true;
    	}
    }
}
