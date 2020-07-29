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
                Refresh();
        }
    }

    void OnEnable()
    {
    	Player.OnPlayerHit += Refresh;
    	if(trackedPlayer!=null)
    		Refresh();
    }

    void OnDisable()
    {
    	Player.OnPlayerHit -= Refresh;
    }

    void Refresh()
    {
    	int lives = trackedPlayer.Lives;
    	for(int i=0; i<hearts.Count; i++){
    		hearts[i].SetActive(i<lives);
    	}
    }
}
