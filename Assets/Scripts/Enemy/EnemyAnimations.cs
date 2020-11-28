using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAnimations : MonoBehaviour
{
	[SerializeField]
	private Animator sprite;
	const float END_OF_DEATH = 0.01f;
    
    void Start()
    {
        if(sprite==null)
        	sprite = GetComponentsInChildren<Animator>()[1];
    }

    public void PlayHit()
    {
    	sprite.SetTrigger("onHit");
    }

    public void PlayDeath()
    {
    	sprite.SetTrigger("onDeath");
    }

    public float DeathAnimDuration()
    {
    	AnimationClip[] clips = sprite.runtimeAnimatorController.animationClips;
    	foreach(var c in clips){
    		if(c.name=="Die"){
    			return c.length-END_OF_DEATH;
    		}
    	}
    	return 0;
    }
}
