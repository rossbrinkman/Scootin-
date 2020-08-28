using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    Rigidbody[] rigidbodies;
    Collider[] colliders;

    public bool death = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();

        setRigidbodies(true);
        setColliders(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (death)
        {
            Die();
            death = false;
        }
    }

    public void Die()
    {
        GetComponent<Animator>().enabled = false;
        setRigidbodies(false);
        setColliders(true);
    }

    void setRigidbodies(bool state)
    {
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = state;
        }
    }

    void setColliders (bool state)
    {
        foreach (Collider c in colliders)
        {
            c.enabled = state;
        }
    }
}
