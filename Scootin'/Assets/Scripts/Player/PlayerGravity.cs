using UnityEngine;

//By Seth
//This script is responsible for applying a downward force onto the player when not grounded.
public class PlayerGravity : MonoBehaviour
{
    [Tooltip("A vertical force applied to the player.")]
    public float gravity = -9.81f;

    //What will be the player's vertical velocity
    [HideInInspector]
    public Vector3 velocity;

    //references
    private CharacterController controller;
    private GroundCheck groundCheck;
    private PlayerStateManager stateManager;

    void Awake()
    {
        CheckForComponentErrors();

        //Start our velocity consistent with our method to keep the player state accurate.
        velocity = new Vector3(0, -2f, 0);
    }

    void Update()
    {
        if (stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
            ApplyGravity();
        
        CheckPlayerState();
    }

    private void ApplyGravity()
    {
        //We are always applying gravity.
        velocity.y += gravity * Time.deltaTime;

        //Unless we are grounded, then gravity is greatly reduced.
        if (groundCheck.isGrounded && velocity.y < 0)
        {
            //We want just a little gravity so our player doesn't hover.
            velocity.y = -2f;
        }

        //We multiply by time twice as this more closely simulates gravity.
        controller.Move(velocity * Time.deltaTime);
    }

    private void CheckPlayerState()
    {
        //This is used to see if the player is currently jumping or falling.
        //It is not currently feasible to set the jumping state in Jump.cs due to our ground detection.

        //If velocity, the player should be jumping.
        if (!stateManager.doingTrick && (velocity.y != -2 && stateManager.currentState == PlayerStateManager.PlayerState.Primed) || (velocity.y != -2 && stateManager.currentState == PlayerStateManager.PlayerState.Pushing))
        {
            //print("Oi");
            stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
        }
    }

    private void CheckForComponentErrors()
    {
        if (GetComponent<GroundCheck>() != null)
        {
            groundCheck = GetComponent<GroundCheck>();
        }
        else
        {
            Debug.LogError("The Player is missing the GroundCheck component.");
        }

        if (GetComponent<PlayerStateManager>() != null)
        {
            stateManager = GetComponent<PlayerStateManager>();
        }
        else
        {
            Debug.LogError("The Player is missing the PlayerStateManager component.");
        }

        if (GetComponent<CharacterController>() != null)
        {
            controller = GetComponent<CharacterController>();
        }
        else
        {
            Debug.LogError("The Player is missing the Character Controller component.");
        }
    }
}