using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class HitboxDisplay : MonoBehaviour
{
	public Sprite normalHitbox;
	public Sprite strafingHitbox;
	Player player;
	[SerializeField]
    private SpriteRenderer hitboxSprite;
    void Awake()
    {
        player = GetComponent<Player>();
        if(hitboxSprite==null){
            hitboxSprite = GetComponentsInChildren<SpriteRenderer>()[1];
            Debug.LogWarning("Hitbox not assigned to player. Attempting to auto-assign.");
        }
    }

    void Update()
    {
        switch (player.GetMoveState()){
        	case 1:
        		hitboxSprite.sprite = strafingHitbox;
        		break;
        	default:
        		hitboxSprite.sprite = normalHitbox;
        		break;
        }
    }

    public SpriteRenderer GetSprite()
    {
        return hitboxSprite;
    }
}
