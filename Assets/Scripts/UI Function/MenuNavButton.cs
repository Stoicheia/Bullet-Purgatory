using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuNavButton : MonoBehaviour
{
    public GameObject toMenu;
    public delegate void MenuNavAction(GameObject g);
    public static event MenuNavAction OnClick;

    public void Click()
    {
        OnClick?.Invoke(toMenu);
    }
}
