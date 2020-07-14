using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticles : MonoBehaviour
{
	public ParticleSystem particles;

	void OnEnable()
	{
		Player.OnPlayerHit += Play;
	}

	void OnDisable()
	{
		Player.OnPlayerHit -= Play;
	}

	void Start()
	{
		particles.Stop();
	}

	public void Play(){
		particles.Play();
	}
}
