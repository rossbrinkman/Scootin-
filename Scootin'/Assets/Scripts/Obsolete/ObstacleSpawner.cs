//using System.Collections;
//5/15/2020 This namespace can be removed. -Seth

using System.Collections.Generic;
using UnityEngine;

//5/15/2020 The single responsibility should be listed here. -Seth
//5/15/2020 It should be spelled "obstacle". If you change this, you will also need to change the name of the script. -Seth

//5/15/2020  Fixed -G
//This script will handle randomness of the obsticles that are spawned in the play area of the road when a new platform loads
public class ObstacleSpawner : MonoBehaviour
{
    //This script will handle randomness of the obsticles that are spawned in the play area of the road when a new platform loads

    //5/15/2020 I know you are not done with it, but this script needs more comments overall. -Seth

    [System.Serializable]
    public struct Spawnable
    {

        public GameObject gameObject;

        //5/15/2020 What does weight do? Does it determine how frequently objects appear? Is it relative to other objects? 
        //What range of values should be used? Can you use "[Range(0.0f, 1.0f)]" to  -Seth

            //Weight determens the percentage of chance the item is spawned out from 0 to 100 percent chance.
            //range will be 0 to 100, combine list does not need to equal to 100.
        public float weight;
    }

    //5/15/2020 Should be "obstacles". Highlight this word and press CTRL + R and then CTRL + R, then press backspace, then type the new name and hit apply. -Seth
    public List<Spawnable> obstacles = new List<Spawnable>();

    //5/15/2020 I'm going to stop here as I assume you're still working on this one. -Seth

    float totalWeight;


    //gives the items weight/percentage
    private void Awake()
    {
        totalWeight = 0;
        foreach (var spawnable in obstacles)
        {
            totalWeight += spawnable.weight;
        }
    }

    void Start()
    {
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = obstacles[0].weight;

        while (pick > cumulativeWeight && chosenIndex < obstacles.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += obstacles[chosenIndex].weight;
        }

        GameObject i = Instantiate(obstacles[chosenIndex].gameObject, transform.position, Quaternion.identity) as GameObject;
        Destroy(i, 10f);
    }
}