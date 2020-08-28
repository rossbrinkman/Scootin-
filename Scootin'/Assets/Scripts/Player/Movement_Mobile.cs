using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Mobile : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterController controller;
    private PlayerStateManager stateManager;
    private GroundCheck groundCheck;

    private RaycastHit hit;

    private bool yMaxReached = false;
    private bool yMinReached = false;

    [SerializeField]
    private float maxSpeed = 30f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float acceleration = 0.1f;

    [HideInInspector]
    public float speed;
    private float rotationSpeed = 75;

    //A value that increases over time.
    private float t = 0f;
    private Quaternion defaultRot;
    [HideInInspector]
    public bool transition = false;

    //[HideInInspector]
    //public bool isGrounded = false;
    //
    //[Tooltip("Should be set to the empty GroundCheck child GameObject.")]
    //public Transform groundCheck;
    //
    //[Tooltip("How close the player has to be for the ground to be detected.")]
    //[SerializeField]
    //private float groundDistance = 0.03f;

    // Start is called before the first frame update
    void Start()
    {
        defaultRot = transform.rotation;
        controller = GetComponent<CharacterController>();
        stateManager = GetComponent<PlayerStateManager>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
        {
            MoveForward();
        }

        Rotate();

        if (transition && transform.parent.rotation.x != 0)
        {
           transform.parent.rotation = /*Quaternion.identity;*/Quaternion.Slerp(transform.parent.rotation, Quaternion.identity, Time.fixedDeltaTime * 2.5f);
        }
        else if(!transition && transform.parent.rotation.x != 15)
        {
            transform.parent.rotation = /*defaultRot;*/Quaternion.Slerp(transform.parent.rotation, defaultRot, Time.fixedDeltaTime * 2.5f);
        }

        //Shoot a raycast down from the groundcheck transform.
        //isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance);
        //Debug.DrawRay(groundCheck.position, Vector3.down, Color.red);

            //print(isGrounded);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Mountain")
    //    {
    //        isGrounded = true;
    //        stateManager.ActiveState(PlayerStateManager.PlayerState.Pushing);
    //    }
    //}
    //private void OnCollisionExit(Collision collision)
    //{
    //    isGrounded = false;
    //    stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
    //}

    void Rotate()
    {
        if (stateManager.currentState != PlayerStateManager.PlayerState.Airborne && groundCheck.isGrounded)
        {
            rotationSpeed = 75;
            if (Input.GetKey(KeyCode.D) && !yMaxReached)
            {
                transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
                yMinReached = false;
                if (transform.eulerAngles.y > 45 && transform.eulerAngles.y < 50)
                    yMaxReached = true;
                if (stateManager.currentState == PlayerStateManager.PlayerState.Pushing || stateManager.currentState == PlayerStateManager.PlayerState.Lean_Left)
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Lean_Right);
                else if (stateManager.currentState == PlayerStateManager.PlayerState.Primed || stateManager.currentState == PlayerStateManager.PlayerState.Primed_Left)
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Primed_Right);

            }
            else if (Input.GetKey(KeyCode.A) && !yMinReached)
            {
                transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
                yMaxReached = false;
                if (transform.eulerAngles.y < 315 && transform.eulerAngles.y > 310)
                    yMinReached = true;
                if (stateManager.currentState == PlayerStateManager.PlayerState.Pushing || stateManager.currentState == PlayerStateManager.PlayerState.Lean_Right)
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Lean_Left);
                else if (stateManager.currentState == PlayerStateManager.PlayerState.Primed || stateManager.currentState == PlayerStateManager.PlayerState.Primed_Right)
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Primed_Left);
            }
            else
            {
                rb.angularVelocity = new Vector3(0, 0, 0);
                if (stateManager.currentState != PlayerStateManager.PlayerState.Primed && stateManager.currentState != PlayerStateManager.PlayerState.Manual)
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Pushing);
            }
        }
        else
        {
            rotationSpeed = 25;
            if (Input.GetKey(KeyCode.D) && !yMaxReached)
            {
                transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);
                yMinReached = false;
                if (transform.eulerAngles.y > 45 && transform.eulerAngles.y < 50)
                    yMaxReached = true;
            }
            else if (Input.GetKey(KeyCode.A) && !yMinReached)
            {
                transform.Rotate(0, -Time.deltaTime * rotationSpeed, 0);
                yMaxReached = false;
                if (transform.eulerAngles.y < 315 && transform.eulerAngles.y > 310)
                    yMinReached = true;
            }
            else
                rb.angularVelocity = new Vector3(0, 0, 0);
        }
    }

    private void MoveForward()
    {
        //Gives a value between two numbers over t.
        float z = Mathf.Lerp(0, maxSpeed, t);

        //Store our speed to be accessed for grinding
        speed = z;

        //Increase our acceleration over time.
        t += acceleration * Time.deltaTime;

        transform.parent.position += transform.forward * Time.deltaTime * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Transition")
        {
            transition = true;
            //transform.parent.rotation = /*Quaternion.identity;*/Quaternion.Slerp(transform.rotation, Quaternion.identity, Time.deltaTime * 2.5f);
        }
        if (other.tag == "EndTransition")
        {
            transition = false;
            //transform.parent.rotation = /*defaultRot;*/Quaternion.Slerp(transform.rotation, defaultRot, Time.deltaTime * 2.5f);
        }
    }

}
