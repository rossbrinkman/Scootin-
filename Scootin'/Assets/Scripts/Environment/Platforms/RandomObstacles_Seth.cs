using System.Collections.Generic;
using System.Collections;
using UnityEngine;

//This script is responsible for keeping a list of obstacles,
//getting a random point near a collider,
//picking a random object accounting for rarity,
//and spawning as many of those random objects on the ground at those random points as you want.
public class RandomObstacles_Seth : MonoBehaviour
{
    [Tooltip("How many objects you want to spawn.")]
    [SerializeField]
    private int count;

    private int maxCount;

    //Data structure for the different objects.
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject gameObject;

        [Tooltip("From 0% chance to appear to 100%")]
        [Range(0.0f, 100f)]
        public float rarity;
    }

    //Fill this up with obstacles in the inspector.
    public List<SpawnableObject> obstacles = new List<SpawnableObject>();

    //We want to shoot the raycasts from up in the air in case the object spawns below the collider.
    //This is adjustable in case we have some kind of overhang.
    public Vector3 verticalRaycastOffset = new Vector3(0f, 100f, 0f);

    private float raritySum = 0;

    private Collider boundaries;

    private bool dumbFix = false;

    private void Awake()
    {
        boundaries = GetComponent<Collider>();

        maxCount = count;
    }

    void Start()
    {
        foreach (var obstacle in obstacles)
        {
            //Add up all the rarity values.
            raritySum += obstacle.rarity;
        }
    }

    private void OnEnable()
    {
        if (dumbFix == true)
        {
            count = maxCount;

            while (count > 0)
            {
                count--;

                SpawnRandomObstacle();
            }
        }
        else
        {
            dumbFix = true;
        }
    }

    void SpawnRandomObstacle()
    {
        //Get a random point within the boundaries of a collider and lift it up in the air.
        Vector3 spawnPoint = GetRandomPointNearCollider(boundaries.bounds);

        //Choose an object at random, taking rarity into account.
        int chosenIndex = PickRandomObstacle();

        //Shoot a raycast down.
        RaycastHit hit;
        if (Physics.Raycast(spawnPoint + verticalRaycastOffset, Vector3.down, out hit))
        {
            //Change the spawn point to the point on the ground where it hits.
            spawnPoint = hit.point;

            //Instantiate the given obstacle.
            Instantiate(obstacles[chosenIndex].gameObject, spawnPoint, transform.rotation);
        }

        ////Instantiate the given obstacle.
        //Instantiate(obstacles[chosenIndex].gameObject, spawnPoint, transform.rotation);

    }


    public Vector3 GetRandomPointNearCollider(Bounds bounds)
    {
        return new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                Random.Range(bounds.min.z, bounds.max.z));
    }

    private int PickRandomObstacle()
    {
        int chosenIndex = 0;

        //Pick a random number between 0 and the total of all the rarities.
        float pick = Random.value * raritySum;

        //Find the first obstacle's rarity.
        float cumulativeRarity = obstacles[0].rarity;

        //While a random number (less that the total of all the rarities) is greater than the cumulative rarity so far AND our index is less than the total number of possible obstacles minus one.
        while (pick > cumulativeRarity && chosenIndex < obstacles.Count - 1)
        {
            chosenIndex++;

            //Add the next objects rarity to the previous.
            cumulativeRarity += obstacles[chosenIndex].rarity;
        }

        //I don't like random weighted numbers.
        return chosenIndex;
    }
}


