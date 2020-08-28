using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private ForwardMovement movement;

    private void Start()
    {
        movement = GetComponent<ForwardMovement>();
        //StartCoroutine(GameOverDelay());
    }

    //IEnumerator GameOverDelay()
    //{
    //    yield return new WaitForSeconds(20f);

    //    FindObjectOfType<GameManager>().EndGame();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Boundary")
        {
            movement.enabled = false;

            FindObjectOfType<GameManager>().EndGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ComboMulti")
        {
            ComboManager cm = FindObjectOfType<ComboManager>();
            cm.newComboValue = 2;
            cm.AddToCombo();
            Destroy(other.gameObject);
        }

        if (other.tag == "SpeedBoost")
        {
            //Talk to seth to see what he wants to do about speedboost
            //FindObjectOfType<ForwardMovement>(maxSpeed);
        }
    }
}
