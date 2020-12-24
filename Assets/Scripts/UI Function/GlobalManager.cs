using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalManager : MonoBehaviour
{
    public GameObject LevelLoadingScreen;

    public static GlobalManager instance;
    ItemDatabase itemDatabase;
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
        SceneManager.LoadScene(Mathf.Min(stats.FinalLevelIndex, stats.LastLevelPassed + 1));
    }

    List<AsyncOperation> scenesLoading = new List<AsyncOperation>();
    public void GoToLevel(int level)
    {
        LevelLoadingScreen.SetActive(true);
        scenesLoading.Add(SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + level));
        StartCoroutine(GetSceneLoadProgress());
    }

    IEnumerator GetSceneLoadProgress()
    {
        for(int i=0; i<scenesLoading.Count; i++)
        {
            while(!scenesLoading[i].isDone)
            {
                yield return null;
            }
        }
        LevelLoadingScreen.SetActive(false);
    }

    public void GoToMenu()
    {
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
        SerializationManager.Save("Save", SaveData.current);
    }

    public bool LoadGame()
    {
        SaveData.current = (SaveData)SerializationManager.Load(Application.persistentDataPath + "/saves/Save.save");
        GlobalStats.instance.PlayerData = SaveData.current.playerData;
        return SaveData.current != null;
    }


}