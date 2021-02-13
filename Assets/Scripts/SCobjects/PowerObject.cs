using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="My Power", menuName = "Power")]
[System.Serializable]
public class PowerObject : Equippable
{
    [SerializeField]
    List<Power> effects;

    public List<Power> Effect { get => effects; }
    public void UseEffect()
    {
        foreach(var p in effects)
            p.Activate();
    }
}
