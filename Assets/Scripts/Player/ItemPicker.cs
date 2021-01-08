using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class ItemPicker : MonoBehaviour
{
	public delegate void ItemChangeAction(int amount, Player p);
	public static event ItemChangeAction OnCoinPickup;
	private Player player;
    int coins;

    public float magnetStrength = 2f;

    private void Awake()
    {
	    player = GetComponent<Player>();
    }

    public int Coins{get{return coins;} private set{coins = value;}}

    public void AddCoins(int c){
    	coins += c;
    	if(OnCoinPickup!=null)
    		OnCoinPickup(c, player);
    }

    public void AddHearts(int h)
    {
	    player.ChangeLives(1);
    }

    public void IncreaseMagnetStrengthForSeconds(float s, float t)
    {
	    StartCoroutine(IncreaseMagnetStrengthSequence(s,t));
    }
    
    IEnumerator IncreaseMagnetStrengthSequence(float newStrength, float time)
    {
	    float originalMagnetStrength = magnetStrength;
	    magnetStrength = newStrength;
	    yield return new WaitForSeconds(time);
	    magnetStrength = originalMagnetStrength;
    }
}
