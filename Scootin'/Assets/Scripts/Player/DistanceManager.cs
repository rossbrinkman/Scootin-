using UnityEngine;
using System.Collections;

public class DistanceManager : MonoBehaviour
{
    //private float totalDistance = 0;
    public int itotalDistance = 0;
    //private bool record = true;
    //private Vector3 previousLoc;


    //void FixedUpdate()
    //{
    //    if (record) 
    //    {
    //        StopCoroutine(Miles());
    //        StartCoroutine(Miles());              
    //    }

    //}

    //IEnumerator Miles()
    //{
    //    yield return new WaitForSeconds(1f);
    //    RecordDistance();
    //}

    //void RecordDistance()
    //{
    //    totalDistance += Vector3.Distance(transform.position, previousLoc);
    //    itotalDistance = (int)totalDistance - 30;

    //    previousLoc = transform.position;
    //}

    //void ToggleRecord() => record = !record;

    public Vector3 oldPos;

    float totalDistance = 0;

    void Start()
    {
        oldPos = transform.position;
    }
    void FixedUpdate()
    {
        itotalDistance = (int)totalDistance;
        Vector3 distanceVector = transform.position - oldPos;
        float distanceThisFrame = distanceVector.magnitude;
        totalDistance += distanceThisFrame;
        oldPos = transform.position;
    }
}
