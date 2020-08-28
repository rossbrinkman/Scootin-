using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenerySpawner : MonoBehaviour
{
    //This script will handle the randomness of the trees/buildings that are spawned on the sides of the road when a new platform loads

    [System.Serializable]
    public struct Spawnable
    {

        public GameObject gameObject;

        public float weight;
    }

    public List<Spawnable> items = new List<Spawnable>();

    float totalWeight;


    //gives the items weight/percentage
    private void Awake()
    {
        totalWeight = 0;
        foreach (var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }

    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += items[chosenIndex].weight;
        }

        GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
        Destroy(i, 10f);
    }
}
