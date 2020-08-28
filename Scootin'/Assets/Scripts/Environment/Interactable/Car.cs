using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public List<Transform> pointTransforms;
    public List<Vector3> carPoints;

    private Rigidbody rb;
    private Transform nextPoint;
    private Vector3 spawnPosition, previousPoint;
    private int randomNumber;
    private float interpolationRatio;
    private bool objectHit, haveChecked = false;

    // Start is called before the first frame update
    void Start()
    {
        objectHit = false;

        if(GetComponent<Rigidbody>() != null)
            rb = GetComponent<Rigidbody>();

        foreach (Transform trans in pointTransforms)
        {
            carPoints.Add(trans.position);
        }

        do
            randomNumber = Random.Range(0, carPoints.Count);
        while (randomNumber == 0 || randomNumber == 11);

        spawnPosition = carPoints[randomNumber];
        previousPoint = spawnPosition;
        //print(randomNumber);

        Vector3 newRotation;
        if (IsBetween(randomNumber, 1, 4))
        {
            newRotation = new Vector3(-15, 180, 0);
            nextPoint = pointTransforms[0];
        }
        else if (IsBetween(randomNumber, 5, 6))
        {
            newRotation = new Vector3(0, -90, -15);
            nextPoint = pointTransforms[4];
        }
        else if (IsBetween(randomNumber, 7, 10))
        {
            newRotation = new Vector3(15, 0, 0);
            nextPoint = pointTransforms[6];
        }
        else if (randomNumber == 0 || randomNumber == 11)
            newRotation = new Vector3(0, 90, 15);
        else
            newRotation = Vector3.zero;

        transform.position = spawnPosition;
        transform.eulerAngles = newRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!objectHit)
        {
            interpolationRatio += Time.fixedDeltaTime / 15f;

            transform.position = Vector3.Lerp(previousPoint, nextPoint.position, interpolationRatio);
        }

        float distance1 = Vector3.Distance(carPoints[0], transform.position);
        float distance2 = Vector3.Distance(carPoints[4], transform.position);
        float distance3 = Vector3.Distance(carPoints[6], transform.position);
        float distance4 = Vector3.Distance(carPoints[10], transform.position);


        if (distance1 < .5f && !haveChecked)
        {
            interpolationRatio = 0;
            transform.position = carPoints[0];
            previousPoint = transform.position;
            nextPoint = pointTransforms[10];
            transform.LookAt(nextPoint);
            transform.eulerAngles = new Vector3(0, 90, 15);
            haveChecked = true;
            StartCoroutine(WaitUntilGone());
        }
        else if (distance2 < .5f && !haveChecked)
        {
            interpolationRatio = 0;
            transform.position = carPoints[4];
            previousPoint = transform.position;
            nextPoint = pointTransforms[0];
            transform.LookAt(nextPoint);
            transform.eulerAngles = new Vector3(-15, 180, 0);
            haveChecked = true;
            StartCoroutine(WaitUntilGone());
        }
        else if (distance3 < .5f && !haveChecked)
        {
            interpolationRatio = 0;
            transform.position = carPoints[6];
            previousPoint = transform.position;
            nextPoint = pointTransforms[4];
            transform.LookAt(nextPoint);
            transform.eulerAngles = new Vector3(0, -90, -15);
            haveChecked = true;
            StartCoroutine(WaitUntilGone());
        }
        else if (distance4 < .5f && !haveChecked)
        {
            interpolationRatio = 0;
            transform.position = carPoints[10];
            previousPoint = transform.position;
            nextPoint = pointTransforms[6];
            transform.LookAt(nextPoint);
            transform.eulerAngles = new Vector3(15, 0, 0);
            haveChecked = true;
            StartCoroutine(WaitUntilGone());
        }
    }

    IEnumerator WaitUntilGone()
    {
        yield return new WaitForSeconds(5);
        haveChecked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Mountain" || other.tag != "platTrigger")
        {
            //print("hit" + other.name);
            objectHit = true;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Mountain" || other.gameObject.tag != "platTrigger")
        {
            //print("hit" + other.name);
            objectHit = true;
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    public bool IsBetween(double testValue, double bound1, double bound2)
    {
        return (testValue >= bound1 && testValue <= bound2);
    }
}
