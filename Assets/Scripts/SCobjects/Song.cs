using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Untitled", menuName = "Song")]
public class Song : Equippable
{
    [SerializeField] Sprite inactiveIcon;
    [SerializeField] RhythmMap rhythmMap;


    public Sprite InactiveIcon { get => inactiveIcon; }
    public RhythmMap RhythmMap { get => rhythmMap; }


}
