using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTraffic : MonoBehaviour
{
    public List<GameObject> cars;
    private List<GameObject> spawnedCars;
    private List<Rigidbody> rbs;

    private int nameOfObject;
    private int i = 0;
    [HideInInspector]
    public bool firstSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnedCars = new List<GameObject>();
        rbs = new List<Rigidbody>();
        nameOfObject = System.Convert.ToInt32(this.name);

        //GetAllRigidbodies();
        SpawnNewCar();
        firstSpawn = false;
        SendCar();
    }

    // Update is called once per frame
    void Update()
    {
        //if ()
        //{
        //    i = Random.Range(0, spawnedCars.Count - 1);
        //    SendCar();
        //}
    }

    //void GetAllRigidbodies()
    //{
    //    foreach (GameObject c in spawnedCars)
    //    {
    //        Rigidbody r = c.GetComponent<Rigidbody>();
    //        if(r != null)
    //            rbs.Add(r);
    //    }
    //}
   
    public void SendCar()
    {
        i = Random.Range(0, spawnedCars.Count);
        Vector3 rotation = Vector3.zero;
        if (nameOfObject < 3)
            rotation = new Vector3(0, -90, 0);
        else if (nameOfObject >= 3)
            rotation = new Vector3(0, 90, 0);
        spawnedCars[i].transform.eulerAngles = rotation;
        spawnedCars[i].SetActive(true);
        Rigidbody rig = spawnedCars[i].GetComponent<Rigidbody>();
        if (rig != null)
            rig.AddForce(Vector3.forward * 10);
        else
        {
            rig = spawnedCars[i].GetComponentInChildren<Rigidbody>();
            rig.AddForce(Vector3.forward * 10);
        }
    }

    void SpawnNewCar()
    {
        foreach (GameObject car in cars)
        {
            float positionYOffset = 0;
            switch (car.name)
            {
                case "School Bus Variant":
                    positionYOffset = .75f;
                    break;
                case "White Moving Truck Variant":
                    positionYOffset = .7f;
                    break;
                case "Blue Truck Variant":
                    positionYOffset = -.3f;
                    break;
                case "Maroon Truck with Bed Variant":
                    positionYOffset = -.3f;
                    break;
                case "White Moving Truck Variant 1":
                    positionYOffset = .7f;
                    break;
                default:
                    positionYOffset = 0;
                    break;
            }

            GameObject newVehicle = Instantiate(car, new Vector3(transform.position.x, transform.position.y + positionYOffset, transform.position.z), Quaternion.identity) as GameObject;
            spawnedCars.Add(newVehicle);
            newVehicle.transform.parent = this.transform;
            newVehicle.SetActive(false);
        }
    }

    public void SpawnNewCarPartTwo()
    {
        foreach (GameObject c in spawnedCars)
        {
            float positionYOffset = 0;
            switch (c.name)
            {
                case "School Bus Variant(Clone)":
                    positionYOffset = .725f;
                    break;
                case "White Moving Truck Variant(Clone)":
                    positionYOffset = .7f;
                    break;
                case "Blue Truck Variant(Clone)":
                    positionYOffset = -.3f;
                    break;
                case "Maroon Truck with Bed Variant(Clone)":
                    positionYOffset = -.3f;
                    break;
                case "White Moving Truck Variant 1(Clone)":
                    positionYOffset = .7f;
                    break;
                default:
                    positionYOffset = 0;
                    break;
            }

            c.transform.position = new Vector3(transform.position.x, transform.position.y + positionYOffset, transform.position.z);
            c.gameObject.SetActive(false);
        }
    }
}