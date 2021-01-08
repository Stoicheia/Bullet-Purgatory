using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] Transform levelButtonsContainer;
    GlobalStats stats;

    private void Start()
    {
        //stats = GlobalStats.instance;
        //Refresh();
    }

    private void OnEnable()
    {
        stats = GlobalStats.instance;
        Refresh();
    }

    public void Refresh()
    {
        foreach(Transform buttonTransform in levelButtonsContainer)
        {
            LevelSelectButton button = buttonTransform.GetComponent<LevelSelectButton>();
            if (button == null) continue;
            button.SetLocked(stats.LastLevelPassed+2 <= button.level);
            button.Refresh();
        }
    }
}
