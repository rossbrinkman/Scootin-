using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Seth
//This script is responsible for the jump mechanic.
public class Jump : MonoBehaviour
{
    public KeyCode jump;

    [Tooltip("Minimum jump height.")]
    [SerializeField]
    [Range(1f, 15f)]
    public float minimumJumpHeight = 15;

    [Tooltip("Maximum jump height.")]
    [SerializeField]
    [Range(1f, 30)]
    public float maxJumpHeight = 30f;

    [Tooltip("How quickly you reach the maximum jump height.")]
    [SerializeField]
    [Range(1f, 5f)]
    private float buildUpSpeed = 3;

    [Header("Do not change. For viewing only.")]

    [SerializeField]
    [Tooltip("Do not adjust this, it is just here to view this element.")]
    private float jumpHeight = 15;

    private GroundCheck groundCheck;
    private PlayerStateManager stateManager;
    private TimeManager timeManager;
    private Rigidbody rig;
    private Death death;

    public AudioManager audioManager;
    private AudioSource audioSource;

    [HideInInspector]
    public bool primed;
    private bool powerUp = false, didOnce = false;

    void Awake()
    {
        death = GetComponent<Death>();
        rig = GetComponent<Rigidbody>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        stateManager = GetComponent<PlayerStateManager>();
        timeManager = GetComponent<TimeManager>();
        audioSource = GetComponent<AudioSource>();

        jumpHeight = minimumJumpHeight;
    }

    // Update is called once per frame
    void Update()
    {
        //This is structured for easy controller support.
        if (!stateManager.manual)
        {
            if (Input.GetKey(jump))
            {
                if (powerUp && !didOnce && groundCheck.isGrounded)
                {
                    minimumJumpHeight = 15; maxJumpHeight = 30; jumpHeight = 15;
                    didOnce = true;
                }
                Crouch();
            }
        }
        if (Input.GetKeyUp(jump))
        {
            JumpMethod();
        }
    }

    private void Crouch()
    {
        if (jumpHeight < maxJumpHeight)
            jumpHeight += Time.deltaTime * buildUpSpeed;
        
        if(stateManager.currentState != PlayerStateManager.PlayerState.Primed && stateManager.currentState != PlayerStateManager.PlayerState.Airborne && stateManager.currentState != PlayerStateManager.PlayerState.Primed_Right && stateManager.currentState != PlayerStateManager.PlayerState.Primed_Left)
        {
            stateManager.ActiveState(PlayerStateManager.PlayerState.Primed);
        }      
    }

    private void JumpMethod()
    {
        if (groundCheck.isGrounded && !death.dead)
        {
            //We apply some physics to our velocity to make the player jump.

            rig.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);

            stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);

            audioSource.clip = audioManager.clips[2];
            audioSource.Play();

            if (powerUp)
            {
                powerUp = false; didOnce = false;
                minimumJumpHeight = 8;
                maxJumpHeight = 16;
                jumpHeight = 8;
            }
            //timeManager.DoSlowMotion();
        }
        //else
        //    jumpUpSound = false;

        jumpHeight = minimumJumpHeight;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PowerJump")
        {
            powerUp = true;
        }
    }
}