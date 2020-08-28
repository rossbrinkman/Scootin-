using System.Collections.Generic;
using UnityEngine;


//This script will allow for the random spawning of obstacles within the confines of a collider and what spawns based on a percentage -G
public class RandomObstacles : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {

        public GameObject gameObject;


        //Weight determens the percentage of chance the item is spawned out from 0 to 100 percent chance.-G
        //range will be 0 to 100, combine list does not need to equal to 100.-G
        public float weight;
    }

    public List<Spawnable> obstacles = new List<Spawnable>();

    public Collider Boundaries;
    private Vector3 spawnPoint;

    float totalWeight;

    private int listLength;


    //random points
    public Vector3 point;

    float xPoint;
    float zPoint;
    float yPoint;
    float hypotenuse;



    private void Awake()
    {
        //gets the size of the array
        listLength = obstacles.Count - 1;

        totalWeight = 0;
        foreach (var spawnable in obstacles)
        {
            totalWeight += spawnable.weight;
        }
    }

    void Update()
    {
        if (listLength >= 0)
        {
            listLength--;

            Spawn();
        }


        point = GetRandomPointInCollider(Boundaries.bounds);

        xPoint = point.x;
        zPoint = point.z;
        float adjustedZPoint = zPoint - Boundaries.bounds.min.z;
        print("z point: " + (adjustedZPoint));

        hypotenuse = (adjustedZPoint / (Mathf.Cos(0.2618f)));
        print("hyp: " + hypotenuse);

        yPoint = (-Mathf.Sqrt( Mathf.Pow(hypotenuse, 2) - Mathf.Pow(adjustedZPoint, 2))) + 14;
        print("ypoint: " +yPoint);




    }

    //sets the min and max boundaries of where things can be spawned
    public static Vector3 GetRandomPointInCollider(Bounds bounds)
    {

        return new Vector3((Random.Range(bounds.min.x, bounds.max.x) + -bounds.min.x), (Random.Range(bounds.min.y, bounds.max.y) + -bounds.min.y), Random.Range(bounds.min.z, bounds.max.z));

    }

    void Spawn()
    {
        spawnPoint = new Vector3(xPoint, yPoint, zPoint);

        //spawnPoint = GetRandomPointInCollider(Boundaries.bounds);

        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = obstacles[0].weight;

        while (pick > cumulativeWeight && chosenIndex < obstacles.Count - 1)
        {
            chosenIndex++;
            cumulativeWeight += obstacles[chosenIndex].weight;
        }

        GameObject i = Instantiate(obstacles[chosenIndex].gameObject, spawnPoint, transform.rotation) as GameObject;
    }

}
