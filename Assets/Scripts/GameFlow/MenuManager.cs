using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Bullet.SetStage(GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>());
        Bullet.SetPooler(FindObjectOfType<ObjectPooler>());
    }

    void Update()
    {
        
    }
}
