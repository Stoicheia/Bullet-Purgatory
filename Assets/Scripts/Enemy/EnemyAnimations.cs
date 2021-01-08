using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAnimations : MonoBehaviour
{
	public Color hitColor = new Color(1,0.5f,0.5f);
	[SerializeField]
	private Animator spriteAnimator;

	private Coroutine previousHitGlow;

	private List<SpriteRenderer> sprites;
	const float END_OF_DEATH = 0.01f;
    
    void Start()
    {
        if(spriteAnimator==null)
        	spriteAnimator = GetComponentsInChildren<Animator>()[1];
        sprites = spriteAnimator.GetComponentsInChildren<SpriteRenderer>().ToList();
    }

    public void PlayHit()
    {
    	//sprite.SetTrigger("onHit");
        if(previousHitGlow!=null)
			StopCoroutine(previousHitGlow);
        previousHitGlow = StartCoroutine(HitGlow(0.2f));
    }

    public void PlayDeath()
    {
    	spriteAnimator.SetTrigger("onDeath");
    }

    public float DeathAnimDuration()
    {
    	AnimationClip[] clips = spriteAnimator.runtimeAnimatorController.animationClips;
    	foreach(var c in clips){
    		if(c.name=="Die"){
    			return c.length-END_OF_DEATH;
    		}
    	}
    	return 0;
    }

    IEnumerator HitGlow(float totalTime)
    {
	    float startTime = Time.time;
	    while (Time.time - startTime <= totalTime)
	    {
		    foreach (var sprite in sprites)
		    {
			    sprite.color = Color.Lerp(Color.white, hitColor, Mathf.Sin(Mathf.PI*(Time.time-startTime)/totalTime));
		    }

		    yield return null;
	    }
		
	    foreach (var sprite in sprites)
	    {
		    sprite.color = Color.white;
	    }
    }
}
