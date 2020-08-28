using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Seth
//This script is responsible for checking if the player is grounded.
//It also finds the ground normal of the object underneath you.
public class GroundCheck : MonoBehaviour
{
    //A public value to be seen by other scripts.
    [HideInInspector]
    public bool isGrounded = false;
    [HideInInspector]
    public bool isOnObstacle = false;

    //The ground normal is used in ForwardMovement to apply forward locomotion along the ground slope.
    [HideInInspector]
    public Vector3 groundNormal;

    [Tooltip("Should be set to the empty GroundCheck child GameObject.")]
    public Transform groundCheck;

    [Tooltip("How close the player has to be for the ground to be detected.")]
    [SerializeField]
    private float groundDistance = 0.01f;

    //Where the raycast hits the ground.
    private RaycastHit hit;

    public AudioManager audioManager;
    private AudioSource audioSource;

    private PlayerStateManager stateManager;
    private PlayerGravity playerGravity;
    private HorizontalMovement horizontalMovement;
    private Death death;
    private ComboManager comboManager;
    private GameOver gameOver;
    private float t = 0;
    private bool jumpSuccess = false;

    private void Awake()
    {
        comboManager = GetComponentInParent<ComboManager>();
        audioSource = GetComponent<AudioSource>();
        gameOver = GetComponent<GameOver>();
        death = GetComponentInParent<Death>();
        horizontalMovement = GetComponent<HorizontalMovement>();
        stateManager = GetComponentInParent<PlayerStateManager>();
        playerGravity = GetComponent<PlayerGravity>();
    }
    private void Update()
    {
        if (!isGrounded)
        {
            t += Time.deltaTime;
            if(t > 1.5f)
                jumpSuccess = true;
        }
        else
            t = 0;
    }

    //void update()
    //{
    //    //Shoot a raycast down from the groundcheck transform.
    //    isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, out hit, groundDistance);
    //    print(isGrounded);
    //    //if (isGrounded)
    //    //{
    //    //
    //    //    //Store the normal of the surface we are on.
    //    //    groundNormal = hit.normal;
    //    //
    //    //    if (playerGravity.velocity.y == -2 && stateManager.currentState != PlayerStateManager.PlayerState.Pushing && stateManager.currentState != PlayerStateManager.PlayerState.Primed && stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
    //    //    {
    //    //        if (stateManager.doingTrick == true) //If they are doing a trick
    //    //        {
    //    //            //TODO DEATH/FAILURE/RAGDOLL METHOD HERE
    //    //            Debug.Log("The player failed to complete their trick before landing.");
    //    //            stateManager.ActiveState(PlayerStateManager.PlayerState.Ragdoll);
    //    //            death.KillPlayer();
    //    //            death.End();
    //    //        }               
    //    //        else if (!horizontalMovement.leaning && !Input.GetKey("down"))
    //    //            stateManager.ActiveState(PlayerStateManager.PlayerState.Pushing);
    //    //        //And tell the state manager that the player is on the ground, pushing their scooter.
    //    //    }
    //    //    else if (!horizontalMovement.leaning && Input.GetKey("down") && playerGravity.velocity.y == -2 && stateManager.currentState == PlayerStateManager.PlayerState.Pushing && stateManager.currentState != PlayerStateManager.PlayerState.Primed && stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
    //    //    {
    //    //        stateManager.ActiveState(PlayerStateManager.PlayerState.Manual);
    //    //    }
    //    //
    //    //    if (hit.transform.tag == "Ramp")
    //    //    {
    //    //        audioSource.clip = audioManager.clips[5];
    //    //        audioSource.Play();
    //    //    }
    //    //}
    //    //else if (stateManager.currentState == PlayerStateManager.PlayerState.Grinding)
    //    //    stateManager.currentState = PlayerStateManager.PlayerState.Airborne;
    //}

    private void OnTriggerEnter(Collider other)
    {
        //print(other.tag);
        if (other.tag == "Mountain" || other.tag == "Ramp" || other.tag == "Untagged" && !death.dead)
        {
            isGrounded = true;
            if (stateManager.currentState != PlayerStateManager.PlayerState.Pushing && stateManager.currentState != PlayerStateManager.PlayerState.Lean_Left && stateManager.currentState != PlayerStateManager.PlayerState.Lean_Right && stateManager.currentState != PlayerStateManager.PlayerState.Primed &&
                stateManager.currentState != PlayerStateManager.PlayerState.Primed_Left && stateManager.currentState != PlayerStateManager.PlayerState.Primed_Right && stateManager.currentState != PlayerStateManager.PlayerState.Manual)
                stateManager.ActiveState(PlayerStateManager.PlayerState.Pushing);
            if (other.tag == "Ramp")
            {
                audioSource.clip = audioManager.clips[5];
                audioSource.Play();
            }
            if (jumpSuccess)
            {
                comboManager.newComboValue = 1;
                comboManager.AddToCombo();
                jumpSuccess = false;
            }
        }
        if (other.tag == "Obstacle")
        {
            isOnObstacle = true;
            if (stateManager.currentState != PlayerStateManager.PlayerState.Pushing && stateManager.currentState != PlayerStateManager.PlayerState.Lean_Left && stateManager.currentState != PlayerStateManager.PlayerState.Lean_Right && stateManager.currentState != PlayerStateManager.PlayerState.Primed &&
                stateManager.currentState != PlayerStateManager.PlayerState.Primed_Left && stateManager.currentState != PlayerStateManager.PlayerState.Primed_Right && stateManager.currentState != PlayerStateManager.PlayerState.Manual)
                stateManager.ActiveState(PlayerStateManager.PlayerState.Pushing);
        }

        if (stateManager.doingTrick == true && isGrounded) //If they are doing a trick
        {
            //TODO DEATH/FAILURE/RAGDOLL METHOD HERE
            Debug.Log("The player failed to complete their trick before landing.");
            stateManager.ActiveState(PlayerStateManager.PlayerState.Ragdoll);
            death.KillPlayer();
            death.End();
            stateManager.doingTrick = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mountain" || other.tag == "Ramp" || other.tag == "Untagged")
        {
            isGrounded = true;
        }
        if (other.tag == "Obstacle")
        {
            isOnObstacle = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
        isOnObstacle = false;
        StartCoroutine(Airborn());
        //stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
    }

    IEnumerator Airborn()
    {
        yield return new WaitForSeconds(.5f);
        if (!isGrounded && stateManager.currentState != PlayerStateManager.PlayerState.Airborne)
            stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
    }
}