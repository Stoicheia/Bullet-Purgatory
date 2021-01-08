using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    List<GameObject> hearts = new List<GameObject>();
    Player trackedPlayer;

    void Awake()
    {
    	foreach(Transform child in transform)
    		hearts.Add(child.gameObject);
    }

    void Start()
    {

    }

    void Update()
    {
        if(trackedPlayer==null){
            trackedPlayer = FindObjectOfType<Player>();
            if(trackedPlayer!=null)
                Refresh(trackedPlayer.Lives);
        }
    }

    void OnEnable()
    {
    	Player.OnLivesChange += Refresh;
    	if(trackedPlayer!=null)
    		Refresh(trackedPlayer.Lives);
    }

    void OnDisable()
    {
    	Player.OnLivesChange -= Refresh;
    }

    void Refresh(int l)
    {
    	int lives = l;
    	for(int i=0; i<hearts.Count; i++){
    		hearts[i].SetActive(i<lives);
    	}
    }
}
