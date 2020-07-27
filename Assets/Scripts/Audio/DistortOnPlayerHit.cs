using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DistortOnPlayerHit : MonoBehaviour
{
	public AudioMixer audioMixer;
	[Range(0.01f, 5)]
	public float duration;
	[Range(0,1)]
	public float intensity;

    void OnEnable()
    {
        audioMixer.SetFloat("pitch", 1);
    	Player.OnPlayerHit += Distort;
    }

    void OnDisable()
    {
    	Player.OnPlayerHit -= Distort;
    }

    void Distort()
    {
    	StartCoroutine(PitchChangeSequence());
    }

    IEnumerator PitchChangeSequence()
    {
    	float t = 0;
    	while(t<duration){
    		audioMixer.SetFloat("pitch", 1-intensity*Mathf.Sin(Mathf.PI*t/duration));
    		t += Time.deltaTime;
    		yield return null;
    	}
    	audioMixer.SetFloat("pitch", 1);
    	yield break;
    }
}
