using UnityEngine;

public interface IDamageable
{	
    void TakeHit(float damage, Collision2D col);
}
