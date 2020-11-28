using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	public class Pool
	{
		public GameObject prefab;

		private Queue<GameObject> pooledObjects = new Queue<GameObject>();

		public GameObject Get(){
			if(pooledObjects.Count==0)
				AddToPool(1);
			return pooledObjects.Dequeue();
		}

		private void AddToPool(int toAdd){
			for(int i=0; i<toAdd; i++){
				GameObject obj = Instantiate(prefab);
				obj.SetActive(false);
				pooledObjects.Enqueue(obj);
			}
		}

		public void ReturnToPool(GameObject obj){
			obj.SetActive(false);
			pooledObjects.Enqueue(obj);
		}
	}

	public Dictionary<string, Pool> pools;
    public Dictionary<GameObject, bool> onScreen;

    void Awake()
    {
        pools = new Dictionary<string, Pool>();
        onScreen = new Dictionary<GameObject, bool>();
    }

    private void CreatePool(string tag, GameObject prefab){
    	Pool pool = new Pool();
    	pool.prefab = prefab;
    	pools.Add(tag, pool);
    }


    public GameObject Spawn(GameObject obj, string t, Vector3 pos, Quaternion rot){
        if(!pools.ContainsKey(t))
            CreatePool(t, obj);
        GameObject toSpawn = pools[t].Get();
        toSpawn.SetActive(true);
        toSpawn.transform.position = pos;
        toSpawn.transform.rotation = rot;
        onScreen[toSpawn] = true;
        return toSpawn;
    }

    public void Despawn(GameObject g, string t){
        g.transform.Translate(Vector3.up*1000);
        if(!pools.ContainsKey(t))
            CreatePool(t, g);    	
    	pools[t].ReturnToPool(g);
        onScreen[g] = false;
    }

    public List<GameObject> GetAllActive(){
        List<GameObject> objects = new List<GameObject>();
        foreach(var kvp in onScreen){
            if(kvp.Value) objects.Add(kvp.Key);
        }
        return objects;
    }

}
