using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script will handle the platform object pool, allowing us to reuse gameobjects
public class PlatformPooler : MonoBehaviour
{
    public GameObject blockSpawnPrefab;

    //allows us to access the pool in the inspector -G$
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
        //attempt at Singleton should allow easy access by PlatformSpawnScript
    public static PlatformPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<Pool> pools;


    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //for each pool we create -G$
        foreach (Pool pool in pools)
        {
            //we create a pool full of objects -G$
            Queue<GameObject> objectPool = new Queue<GameObject>();


            //fills the pool with these objects until full(size) and adds it to the queue - G$
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }

    }


    //functionality, pull the object from the queue and spawns into the gameworld -G$
    public GameObject SpawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        
        //turn it off and on to reset it's behavior
        objectToSpawn.SetActive(false);

        objectToSpawn.SetActive(true);

        TransitionTraffic[] carSpawns = objectToSpawn.GetComponentsInChildren<TransitionTraffic>();

        foreach (TransitionTraffic tt in carSpawns)
        {
            if (!tt.firstSpawn)
            {
                tt.SpawnNewCarPartTwo();
                tt.SendCar();
            }
        }
        //objectToSpawn.GetComponentInChildren<RandomSpawnWithBlockPoints>().ResetBlocks();

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }


        //adds back to queue to use later
        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }


}
