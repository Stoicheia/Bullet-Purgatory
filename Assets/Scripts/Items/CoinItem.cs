using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : Item
{
    public int value;

    public override void InvokeEffect(ItemPicker player){
    	player.AddCoins(value);
    }
}
