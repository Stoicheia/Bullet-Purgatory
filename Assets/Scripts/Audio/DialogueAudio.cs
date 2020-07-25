using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DialogueAudio : MonoBehaviour
{
    AudioSource audioPlayer;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
    	LevelStateManager.OnStateChange += ToggleAudio;
    }

    void OnDisable()
    {
    	LevelStateManager.OnStateChange -= ToggleAudio;
    }

    void ToggleAudio(LevelState state)
    {
    	if(audioPlayer==null)
    		audioPlayer = GetComponent<AudioSource>();
    	if(state==LevelState.DIALOGUE){
    		audioPlayer.Play();
        }
    	else
    		audioPlayer.Stop();
    }
}
