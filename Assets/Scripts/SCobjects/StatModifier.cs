using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "Upgrade")]
public class StatModifier : Equippable
{
    [SerializeField] Upgrade effect;
    [SerializeField] string signature;
    public Upgrade Effect { get => effect; }
    public string Signature { get => signature; }

    public void Apply(Player player)
    {
        effect.Activate(player);
    }
}
