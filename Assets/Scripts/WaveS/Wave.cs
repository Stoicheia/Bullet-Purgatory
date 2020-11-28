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
}
