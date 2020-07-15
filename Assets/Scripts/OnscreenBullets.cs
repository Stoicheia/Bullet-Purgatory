using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnscreenBullets : MonoBehaviour
{
	[Range(0.01f,10)]
    public float duration;
    [Range(0.01f, 0.99f)]
    public float intensity;
    public ObjectPooler pooler;

    void Start()
    {
        
    }

    void OnEnable()
    {
    	Player.OnPlayerHit += SlowBullets;
    }

    void OnDisable()
    {
    	Player.OnPlayerHit -= SlowBullets;
    }

    void SlowBullets()
    {
    	StartCoroutine(SlowBulletSequence());
    }

    IEnumerator SlowBulletSequence()
    {
    	Bullet.UniversalSpeedModifier *= 1-intensity;
    	yield return new WaitForSeconds(duration);
    	Bullet.UniversalSpeedModifier *= 1/(1-intensity);
    }
}
