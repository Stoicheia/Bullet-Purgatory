using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    GlobalStats stats;

    [SerializeField] private List<Transform> levelSubMenus;
    private Transform activeSubMenu;
    private List<int> worldLevelCounts;

    private void OnEnable()
    {
        stats = GlobalStats.instance;
        
        worldLevelCounts = new List<int>();
        for(int i=0; i < levelSubMenus.Count;i++)
        {
            Transform levelContainer = levelSubMenus[i].Find("Levels");
            worldLevelCounts.Add(levelContainer.childCount);
        }

        int world = CalculateCurrentWorld(stats.LastLevelPassed);
        foreach (Transform t in levelSubMenus)
        {
            t.gameObject.SetActive(false);
        }
        activeSubMenu = levelSubMenus[world - 1];
        activeSubMenu.gameObject.SetActive(true);
        
        RefreshLevels(activeSubMenu);

        LevelSelNavButton.OnNav += GoToSubmenu;
    }

    private void OnDisable()
    {
        LevelSelNavButton.OnNav -= GoToSubmenu;
    }

    public void RefreshLevels(Transform container)
    {
        foreach(Transform buttonTransform in container)
        {
            LevelSelectButton button = buttonTransform.GetComponent<LevelSelectButton>();
            if (button == null) continue;
            button.SetLocked(stats.LastLevelPassed+2 <= button.level);
            button.Refresh();
        }
    }

    int CalculateCurrentWorld(int level)
    {
        int k = level;
        for (int i = 0; i < worldLevelCounts.Count; i++)
        {
            k -= worldLevelCounts[i];
            if (k < 0)
            {
                return i+1;
            }
        }

        return worldLevelCounts.Count;
    }

    void GoToSubmenu(int i)
    {
        if (i >= levelSubMenus.Count)
        {
            Debug.LogWarning("Index exceeds number of submenus. Nothing will happen.");
            return;
        }
        activeSubMenu.gameObject.SetActive(false);
        levelSubMenus[i].gameObject.SetActive(true);
        activeSubMenu = levelSubMenus[i];
    }
}
