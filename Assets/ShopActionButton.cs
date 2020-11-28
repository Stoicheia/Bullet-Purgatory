using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopActionButton : MonoBehaviour
{
    public delegate void ShopAction(ShopActionButton button);
    public static event ShopAction OnClick;
    public TextMeshProUGUI actionText;

    public void Click()
    {
        OnClick?.Invoke(this);
    }

    public void Display(ItemUIStatus status)
    {
        actionText.text = ItemUI.ButtonStatusText[status];
    }
}
