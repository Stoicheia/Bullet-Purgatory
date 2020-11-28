using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUpgrade : Upgrade
{
	public int livesIncrease;
    public override void Activate(Player player){
    	player.ChangeLives(livesIncrease);
    }
}
