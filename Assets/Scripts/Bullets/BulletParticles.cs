using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class BulletParticles : MonoBehaviour
{
    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    IEnumerator DestroyAfterSecondsSequence(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(gameObject);
    }

    public void DestroyAfterSeconds(float time)
    {
        StartCoroutine(DestroyAfterSecondsSequence(time));
    }

    public void Play()
    {
        particles.Play();
    }

    public void Stop()
    {
        particles.Stop();
    }

    public float GetDuration()
    {
        return particles.main.duration * 2;
    }
}
