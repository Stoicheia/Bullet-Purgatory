using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty Shooter", menuName = "Shooter")]
[System.Serializable]
public class Weapon : Equippable
{
    [SerializeField] RhythmicObject shooter;
    [SerializeField] [Range(1,5)] int rating;

    public int Rating { get => rating; }
    public RhythmicObject Shooter { get => shooter; }
}
