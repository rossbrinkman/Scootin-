using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{

    void Start()
    {
        //objectDestruction();

        DestroyObjectDelayed();
    }

    //IEnumerator objectDestruction()
    //{
    //    yield return new WaitForSeconds(30f);
    //    Destroy(gameObject);
    //}

    void DestroyObjectDelayed()
    {
        Destroy(gameObject, 25f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Mountain")
        {
            Destroy(gameObject);
        }

        else if (other.tag != "Transition")
        {
            Destroy(gameObject);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (other.tag != "Mountain")
    //    {
    //        Destroy(gameObject);
    //    }

    //    else if (other.tag != "Transition")
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
