using UnityEngine;

public interface IOverlap
{	
    void Enter(Collision2D col);
    void Exit(Collision2D col);
    bool IsFriendly();
    bool IsSolid();
}
