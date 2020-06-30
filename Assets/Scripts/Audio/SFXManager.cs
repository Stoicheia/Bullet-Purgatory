using Unity.Audio;
using System;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
	public Sound[] sounds;

	public static SFXManager instance;

    void Awake()
    {
    	if(instance==null)
    		instance = this;
    	else{
    		Destroy(gameObject);
    		return;
    	}
    	DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds){
        	s.source = gameObject.AddComponent<AudioSource>();
        	s.source.clip = s.clip;

        	s.source.volume = s.volume;
        	s.source.pitch = s.pitch;
        }
    }

    public void Play(string name){
    	Sound s = Array.Find(sounds, sound => sound.Name == name);
    	if(s!=null)
    		s.source.Play();
    }
}
