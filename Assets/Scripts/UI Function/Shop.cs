using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    GlobalStats stats;

    private void Start()
    {
        stats = GlobalStats.instance;
    }

    public List<Equippable> shopItems;
    public int ItemsCount { get => shopItems.Count; }

    public void BuyItem(Equippable item)
    {
        stats.Pay(item.Cost);
        stats.AddItem(item);
    }

    public void EquipItem(Equippable item)
    {
        stats.EquipItem(item);
    }

    public void UnequipItem(Equippable item)
    {
        stats.UnequipItem(item);
    }
}
