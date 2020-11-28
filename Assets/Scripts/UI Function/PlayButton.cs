using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButton : MonoBehaviour
{
    public delegate void PlayAction();
    public static event PlayAction OnClick;

    public void Click()
    {
        OnClick?.Invoke();
    }
}
