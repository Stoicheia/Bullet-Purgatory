using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    private int currentLevel;

    public int CurrentLevel
    {
        get => currentLevel;
        set => currentLevel = value;
    }

    public GameObject LevelLoadingScreen;
    public RectTransform loadingBar;

    public static GlobalManager instance;
    ItemDatabase itemDatabase;

    private int mainMenuState = 0;
    public ItemDatabase ItemDatabase { get => itemDatabase; }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        itemDatabase = (ItemDatabase)AssetDatabase.LoadAssetAtPath("Assets/Scriptables/Item Database.asset", typeof(ItemDatabase));
        itemDatabase.UpdateReferences();
    }

    private void Start()
    {
        if (!LoadGame())
        {
            GlobalStats.instance.ResetStats();
        }
    }
    public void PlayGame()
    {
        GlobalStats stats = GlobalStats.instance;
        GoToLevel(Mathf.Min(stats.FinalLevelIndex, stats.LastLevelPassed+1+SceneManager.GetActiveScene().buildIndex));
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void GoToLevel(int level)
    {
        currentLevel = level;
        LevelLoadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + level));
        StartCoroutine(GetSceneLoadProgress());
    }

    private float totalSceneProgress;
    IEnumerator GetSceneLoadProgress()
    {
        for(int i=0; i<scenesLoading.Count; i++)
        {
            while(!scenesLoading[i].isDone)
            {
                totalSceneProgress = 0;
                foreach (AsyncOperation operation in scenesLoading)
                {
                    totalSceneProgress += operation.progress;
                }

                totalSceneProgress = Mathf.Min(1,(totalSceneProgress / (0.9f*scenesLoading.Count)));
                loadingBar.transform.localScale = new Vector3(totalSceneProgress, 1, 1);
                yield return null;
            }
        }
        LevelLoadingScreen.SetActive(false);
    }

    public void GoToMenu(int i)
    {
        mainMenuState = i;
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("bye noob!");
        Application.Quit();
    }

    public void SaveGame()
    {
        SaveData.current.playerData = GlobalStats.instance.PlayerData;
        SaveData.current.levelData = GlobalStats.instance.LevelData;
        SerializationManager.Save("Save", SaveData.current);
    }

    public bool LoadGame()
    {
        GlobalStats stats = GlobalStats.instance;
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
        PlayerData playerData = SaveData.current.playerData;
        stats.PlayerData = playerData ?? new PlayerData(stats.initialPlayerData);
        LevelData levelData = SaveData.current.levelData;
        stats.LevelData = levelData ?? new LevelData();
        return SaveData.current != null;
    }

    public int GetMenuState()
    {
        return mainMenuState;
    }
}