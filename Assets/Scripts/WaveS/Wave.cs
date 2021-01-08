using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave : MonoBehaviour
{
	public string Name;
	int numberOfEnemies;

    public void Spawn(){
    	Instantiate(this, new Vector3(0,0,-5), Quaternion.identity);
    }

    public int GetEnemyCount(){
    	numberOfEnemies = 0;
		foreach(var enemy in GetComponentsInChildren<Spawnable>())
			numberOfEnemies++;
    	return numberOfEnemies;
    }

    public List<string> GetEnemyTags()
    {
        List<string> enemies = new List<string>();
        foreach (var enemy in GetComponentsInChildren<Spawnable>())
            enemies.Add(enemy.GetComponentInChildren<Enemy>().enemyTag);
        return enemies;
    }

    public List<Enemy> GetEnemies()
    {
	    List<Enemy> enemies = new List<Enemy>();
	    foreach (var enemy in GetComponentsInChildren<Spawnable>())
	    {
		    enemies.Add(enemy.GetComponentInChildren<Enemy>());
	    }
	    return enemies;
    }
}
