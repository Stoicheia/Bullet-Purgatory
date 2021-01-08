using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grazebox : MonoBehaviour, IOverlap
{
	public delegate void GrazeAction(float s, Transform grazePos);
	public static event GrazeAction OnGraze;
	private const float GRAZE_SCORE_BASE = 1;

	Player myPlayer;
	SpriteRenderer mySprite;

	public float grazeRate;
	int grazeCount;

	void Awake()
	{
		grazeCount = 0;
		myPlayer = transform.parent.GetComponent<Player>();
		mySprite = GetComponent<SpriteRenderer>();
	}
	
	void OnEnable()
	{
		Player.OnPlayerDeath += Deactivate;
	}

	void OnDisable()
	{
		grazeCount = 0;
		Player.OnPlayerDeath -= Deactivate;
	}

	void Update()
	{
		mySprite.enabled = grazeCount>0;
		if(grazeCount>0){
			OnGraze(Time.deltaTime*grazeRate*grazeCount*GRAZE_SCORE_BASE, transform);
		}
	}

	public void Enter(Collision2D col)
	{
		GrazeOn();
	}

	public void Exit(Collision2D col)
	{
		GrazeOff();
	}

    public bool IsFriendly() => true;
    public bool IsSolid() => false;

    void GrazeOn()
	{
		grazeCount++;
	}

	void GrazeOff()
	{
		grazeCount--;
	}

    void Deactivate()
    {
    	gameObject.SetActive(false);
    }

}
