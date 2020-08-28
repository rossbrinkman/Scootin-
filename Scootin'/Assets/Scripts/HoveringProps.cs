using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script to animate props to hover in the options menu -Guss
public class HoveringProps : MonoBehaviour
{

    public Animator bouncy;
    public GameObject boombox;


    void Start()
    {
        bouncy.SetBool("bouncy", false);
        bouncy = GetComponent<Animator>();
        transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(3.5f, 4.5f, Mathf.PingPong(Time.deltaTime, 1)), transform.position.z);
    }
    void OnMouseOver()
    {
            bouncy.SetBool("bouncy", true);
            Debug.Log("Mouse is over GameObject.");
    }

    void OnMouseExit()
    {
        //The mouse is no longer hovering over the GameObject so output this message each frame
        Debug.Log("Mouse is no longer on GameObject.");
    }
}
