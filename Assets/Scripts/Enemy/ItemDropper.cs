using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ItemDropper : MonoBehaviour
{
	[SerializeField]
    public ItemChancePair[] droppedItems;
    Enemy thisEnemy;

    void Start(){
    	thisEnemy = GetComponent<Enemy>();
    }

    void OnEnable(){
    	thisEnemy = GetComponent<Enemy>();
    	thisEnemy.OnThisDeath += SpawnItem;
    }

    void OnDisable(){
    	thisEnemy.OnThisDeath -= SpawnItem;
    }

    void SpawnItem(){
    	foreach(var droppedItem in droppedItems){
    		if(droppedItem.chance>=Random.Range(0,100))
    			Instantiate(droppedItem.item.gameObject, transform.position, Quaternion.Euler(0,0,-180));
    	}
    }
}

[System.Serializable]
public class ItemChancePair{
	public DroppedItem item;
	public float chance;
}
