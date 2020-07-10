using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class HitboxDisplay : MonoBehaviour
{
	public Sprite normalHitbox;
	public Sprite strafingHitbox;
	Player player;
	SpriteRenderer hitboxSprite;
    void Awake()
    {
        player = GetComponent<Player>();
        hitboxSprite = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        switch (player.GetMoveState()){
        	case -1:
        		hitboxSprite.sprite = strafingHitbox;
        		break;
        	default:
        		hitboxSprite.sprite = normalHitbox;
        		break;
        }
    }
}
