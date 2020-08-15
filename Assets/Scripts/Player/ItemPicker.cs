using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPicker : MonoBehaviour
{
	public delegate void ItemChangeAction(int amount);
	public static event ItemChangeAction OnCoinPickup;

    int coins;
    public int Coins{get{return coins;} private set{coins = value;}}

    public void AddCoins(int c){
    	coins += c;
    	if(OnCoinPickup!=null)
    		OnCoinPickup(coins);
    }
}
