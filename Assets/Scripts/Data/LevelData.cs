using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public List<LevelDatum> levelData;

    public void UpdateScore(int level, float score)
    {
        if (levelData.Count < level + 1)
        {
            levelData.AddRange(new LevelDatum[level+1-levelData.Count]);
        }

        if (levelData[level] == null)
        {
            levelData[level] = new LevelDatum();
        }
        levelData[level].score = score;
    }

    public LevelData()
    {
        levelData = new List<LevelDatum>();
    }
}

[System.Serializable]
public class LevelDatum
{
    public float score = 0;
    public int rating = 0;

    public LevelDatum()
    {
        score = 0;
        rating = 0;
    }
}
