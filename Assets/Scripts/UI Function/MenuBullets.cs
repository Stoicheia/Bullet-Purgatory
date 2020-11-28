using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBullets : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Bullet.SetStage(GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>());
        Bullet.SetPooler(FindObjectOfType<ObjectPooler>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
