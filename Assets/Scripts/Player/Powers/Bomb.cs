using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Power
{
	ObjectPooler pooler;
	const float BOMBED_TRANSPARENCY = 0.15f;
	const float BOMB_ZOOM = 3;

    public override void Activate(){
    	pooler = FindObjectOfType<ObjectPooler>();
    	if(pooler==null) Debug.LogError("Object Pool not found! (Bomb power)");
    	List<GameObject> allActive = pooler.GetAllActive();
    	foreach(var obj in allActive){
    		Bullet bullet = obj.GetComponent<Bullet>();
    		if(bullet==null) return;
			if(!bullet.IsFriendly()&&bullet.GetComponent<Collider2D>().enabled)
			{
					AcceleratingBullet accel = bullet.GetComponent<AcceleratingBullet>();
					if (accel != null) accel.Disable();
					bullet.transform.Rotate(0,0,180);
					bullet.KillAnimator();
					bullet.AddSpeed(BOMB_ZOOM/2);
					bullet.MultiplySpeed(BOMB_ZOOM);
					bullet.GetRenderer().color = new Color(1,1,1,BOMBED_TRANSPARENCY);
					bullet.GetComponent<Collider2D>().enabled = false;
			}
        }
    }
}

