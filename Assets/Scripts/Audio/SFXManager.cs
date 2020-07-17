using Unity.Audio;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    public AudioMixer mixer;
	public Sound[] sounds;

	public static SFXManager instance;
    [Range(0,1)]
    public float sfxVolume = 0.5f;

    void PlayShot () => Play("Shot");
    void PlayHit () => Play("OnHit");

    void OnEnable()
    {
        SpreadShooter.OnShoot += PlayShot;
        CustomSpreadShooter.OnShoot += PlayShot;
        Player.OnPlayerHit += PlayHit;
    }

    void OnDisable()
    {
        SpreadShooter.OnShoot -= PlayShot;
        CustomSpreadShooter.OnShoot -= PlayShot;
        Player.OnPlayerHit -= PlayHit;
    }    

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
    	if(s!=null){
            s.source.volume = s.volume*sfxVolume*2;
    		s.source.Play();
        }
    }

    public void ChangeVolume(float v){
        sfxVolume = v;
    }

    public void ChangeMasterVolume(float v){
        mixer.SetFloat("masterVol", Mathf.Log10(Mathf.Max(v,0.001f))*20);
    }
}
