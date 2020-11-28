using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    GlobalStats stats;
    PlayerLoadoutManager loadouts;
    [SerializeField] Shop shop;
    [SerializeField] GameObject itemsContainer;
    [SerializeField] ItemUI itemUIPrefab;
    [SerializeField] ShopActionButton myButton;
    [SerializeField] GoldIndicatorUI goldText;
    ItemUI activeItemUI;

    private void Start()
    {
        stats = GlobalStats.instance;
        Refresh();
    }
    private void OnEnable()
    {
        ItemUI.OnClick += UIClicked;
        ShopActionButton.OnClick += PerformAction;
    }

    private void OnDisable()
    {
        ItemUI.OnClick -= UIClicked;
        ShopActionButton.OnClick -= PerformAction;
    }

    public void UIClicked(ItemUI buttonClicked)
    {
        activeItemUI = buttonClicked;
        Refresh();
    }

    public void PerformAction(ShopActionButton b)
    {
        if (b != myButton) return;
        switch (activeItemUI.Status)
        {
            case ItemUIStatus.NOTBOUGHT:
                shop.BuyItem(activeItemUI.item);
                break;
            case ItemUIStatus.BOUGHT:
                shop.EquipItem(activeItemUI.item);
                break;
            case ItemUIStatus.EQUIPPED:
                shop.UnequipItem(activeItemUI.item);
                break;
        }
        Refresh();
    }

    public void Refresh()
    {
        Transform container = itemsContainer.transform;
        for(int i=0; i<container.childCount; i++)
        {
            ItemUI button = container.GetChild(i).GetComponent<ItemUI>();
            if (button == null) continue;
            if (shop.ItemsCount <= i)
            {
                button.gameObject.SetActive(false);
                continue;
            }
            Equippable item = shop.shopItems[i];
            button.gameObject.SetActive(true);
            button.item = item;
            button.SetStatus(stats.GetItemStatus(item));
            button.Display(item);
        }
        if(activeItemUI!=null)
            myButton.Display(activeItemUI.Status);
        goldText.Refresh();
    }
}
