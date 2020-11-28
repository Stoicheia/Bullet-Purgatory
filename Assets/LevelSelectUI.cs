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
        stats = GlobalStats.instance;
        Refresh();
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {

    }

    public void Refresh()
    {
        foreach(Transform buttonTransform in levelButtonsContainer)
        {
            LevelSelectButton button = buttonTransform.GetComponent<LevelSelectButton>();
            if (button == null) continue;
            button.SetLocked(stats.LastLevelPassed+2 <= button.level);
            print(stats.ToString());
            button.Refresh();
        }
    }
}
