using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public enum ItemUIStatus
{ 
    none = 0,
    LOCKED,
    NOTBOUGHT,
    BOUGHT,
    EQUIPPED
}
[RequireComponent(typeof(Button), typeof(Image))]
public class ItemUI : MonoBehaviour
{
    public delegate void ClickAction(ItemUI buttonClicked);
    public static event ClickAction OnClick;

    public Equippable item;
    public Image itemIcon;

    [SerializeField] ItemUIStatus status;
    public ItemUIStatus Status { get => status; }
    [Space]
    public Sprite LockedSprite;
    public Sprite NotBoughtSprite;
    public Sprite BoughtSprite;
    public Sprite EquippedSprite;

    Dictionary<ItemUIStatus, Sprite> buttonStatusImage;
    Image image;

    public static Dictionary<ItemUIStatus, string> ButtonStatusText;

    void Awake()
    {
        buttonStatusImage = new Dictionary<ItemUIStatus, Sprite>() {
            {ItemUIStatus.LOCKED, LockedSprite},
            {ItemUIStatus.NOTBOUGHT, NotBoughtSprite},
            {ItemUIStatus.BOUGHT, BoughtSprite},
            {ItemUIStatus.EQUIPPED, EquippedSprite}
        };
        ButtonStatusText = new Dictionary<ItemUIStatus, string>()
        {
            {ItemUIStatus.LOCKED, "Locked!"},
            {ItemUIStatus.NOTBOUGHT, "Buy"},
            {ItemUIStatus.BOUGHT, "Equip"},
            {ItemUIStatus.EQUIPPED, "Unequip"}
        };
        image = GetComponent<Image>();
    }

    void Start()
    {
        if (item)
            Display(item);
    }

    public void Click() => OnClick?.Invoke(this);

    public void SetStatus(ItemUIStatus s)
    {
        status = s;
    }
    public void Display(Equippable item)
    {
        if (status == ItemUIStatus.none)
            status = ItemUIStatus.LOCKED;
        this.item = item;
        //itemName.text = item.Name;
        itemIcon.sprite = item.Icon;
        image.sprite = buttonStatusImage[status];
    }   
}
