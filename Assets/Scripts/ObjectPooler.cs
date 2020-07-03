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

    void Awake()
    {
        pools = new Dictionary<string, Pool>();
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
        return toSpawn;
    }

    public void Despawn(GameObject g, string t){
    	pools[t].ReturnToPool(g);
    }    
}
