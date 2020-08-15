using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class ItemDropper : MonoBehaviour
{
    public DroppedItem[] droppedItems;
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
    		Instantiate(droppedItem.gameObject, transform.position, Quaternion.Euler(0,0,-180));
    	}
    }
}
