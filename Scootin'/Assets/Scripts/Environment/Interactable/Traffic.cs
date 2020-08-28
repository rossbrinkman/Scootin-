using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traffic : MonoBehaviour
{
    private Vector3 startPosition;
    private float speed;
    private Rigidbody rb;
    private bool playerHit = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        speed = Random.Range(5, 25);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!playerHit)
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CarTrigger")
        {
            TransitionTraffic tt = transform.parent.GetComponent<TransitionTraffic>();
            tt.SendCar();
            StartCoroutine(Deactivate());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHit = true;
        }
    }

    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(10);
        this.gameObject.SetActive(false);
    }
}
