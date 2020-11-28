using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="My Power", menuName = "Power")]
public class PowerObject : Equippable
{
    [SerializeField]
    Power effect;

    public Power Effect { get => effect; }
    public void UseEffect()
    {
        effect.Activate();
    }
}
