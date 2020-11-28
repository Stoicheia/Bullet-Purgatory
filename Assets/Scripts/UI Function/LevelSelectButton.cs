using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectButton : MonoBehaviour
{
    public int level;
    public delegate void PlayAction(int i);
    public static event PlayAction OnClick;

    [Space]
    public Sprite lockedButtonSprite;
    public Sprite unlockedButtonSprite;

    bool locked = true;
    Image image;
    Button button;

    private void Start()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    public void Click()
    {
        OnClick?.Invoke(level);
    }

    public void SetLocked(bool b)
    {
        locked = b;
    }

    public void Refresh()
    {
        if(image==null)
            image = GetComponent<Image>();
        if(button==null)
            button = GetComponent<Button>();
        Sprite mySprite = locked ? lockedButtonSprite : unlockedButtonSprite;
        image.sprite = mySprite;
        button.interactable = !locked;
    }
}
