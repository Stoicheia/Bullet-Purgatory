using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGroup : MonoBehaviour
{
    public List<GameObject> menus;
    public GameObject startingMenu;
    GameObject activeMenu;
    public GameObject GetActiveMenu => activeMenu;

    void Start()
    {
        foreach(GameObject menu in menus)
        {
            menu.SetActive(false);
        }
        SetActiveMenu(startingMenu);
    }

    private void OnEnable()
    {
        PlayButton.OnClick += StartGameFromCurrentLevel;
        LevelSelectButton.OnClick += GoToLevel;
        MenuNavButton.OnClick += SetActiveMenu;
    }


    public void SetActiveMenu(GameObject newMenu)
    {
        if (!menus.Contains(newMenu))
        {
            Debug.LogWarning("Not a valid menu, attempting to resolve", this);
            menus.Add(newMenu);
        }
        if(activeMenu!=null)
            activeMenu.SetActive(false);
        activeMenu = newMenu;
        activeMenu.SetActive(true);
    }

    void StartGameFromCurrentLevel()
    {
        GlobalManager.instance.PlayGame();
    }

    void GoToLevel(int i)
    {
        if (i < 0)
        {
            GlobalManager.instance.QuitGame();
            return;
        }
            
        GlobalManager.instance.GoToLevel(i);
    }
}
