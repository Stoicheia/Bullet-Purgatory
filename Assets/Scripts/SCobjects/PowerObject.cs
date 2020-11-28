using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="My Power", menuName = "Power")]
[System.Serializable]
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
