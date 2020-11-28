using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Equippable : ScriptableObject
{
    [SerializeField] protected string id;
    [SerializeField] protected string itemName;
    [TextArea(2,8)]
    [SerializeField] protected string description;
    [SerializeField] protected Sprite icon;
    [SerializeField] protected int cost;

    public string ID { get => id;  }
    public string Name { get => itemName; }
    public string Description { get => description; }
    public Sprite Icon { get => icon; }
    public int Cost { get => cost; }

    public static List<string> GetIDs(List<Equippable> equips)
    {
        List<string> IDs = new List<string>();
        foreach(var equip in equips)
        {
            if (equip == null) continue;
            IDs.Add(equip.ID);
        }
        return IDs;
    }
}
