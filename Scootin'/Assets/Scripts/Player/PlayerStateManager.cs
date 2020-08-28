using UnityEngine;
using System.Collections.Generic;

//By Seth
//This script is responsible for storing the Player's current state, such as Idle, Pushing, Jumping, and Trick States
//and setting a parameter in the animator based on what the current state is.

//Oh my god this script ended up being horrible. 
public class PlayerStateManager : MonoBehaviour
{
    private Animator animator;
    private TrickManager trickManager;
    private TrickManager_Mobile trickManager_Mobile;
    private ComboManager comboManager;
    [HideInInspector]
    public float length;
    private float resetValue = 0;

    //Create a list of player states. Each state is set to an integer starting at 0.

    //Here we have a list of all of the player states. Each one is initialized to an int. Idle=0, Pushing=1, etc.
    [SerializeField]
    public enum PlayerState {Idle, Pushing, Primed, Airborne, Tailwhip, Barspin, One_Eighty_Fakie, Clamp_Touch, Finger_Whip, Tailwhip_Rewind, ThreeSixty, T_Bog, Truck_Driver, ThreeSixty_Tailwhip, Scooter_Flip, Lean_Left, Lean_Right, Grinding, Ragdoll, Primed_Left, Primed_Right, BarTwist, TuckNoHander, Manual };

    [SerializeField]
    public enum PlayerStateCategory {DoingTrick, NotDoingTrick};

    //This stores the players current state. A player can only have one state at a time. Read the player state from this variable. Change this variable by using the Method ActiveState(state) below.
    [HideInInspector]
    public PlayerState currentState;

    [HideInInspector]
    public bool doingTrick;
    [HideInInspector]
    public bool manual = false;

    private void Start()
    {
        comboManager = GetComponent<ComboManager>();
        trickManager = GetComponent<TrickManager>();
        trickManager_Mobile = GetComponent<TrickManager_Mobile>();
        animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        length += Time.deltaTime;
        if (doingTrick || manual)
            CheckIfTrickOver();

        if (Input.GetKeyDown(KeyCode.DownArrow) && currentState == PlayerState.Pushing)
            ActiveState(PlayerStateManager.PlayerState.Manual);

        if (Input.GetKeyUp(KeyCode.DownArrow) && manual)
        {
            manual = false;
            ActiveState(PlayerStateManager.PlayerState.Pushing);
        }
        
    }

    //This is a public method which takes in a desired state and then passes a parameter to the Animator.
    public void ActiveState(PlayerState state)
    {
        if(currentState != PlayerState.Ragdoll && !doingTrick)
            switch (state)
            {
                case PlayerState.Idle:
                    {
                        //This state is not set by anthing yet.
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Pushing:
                    {
                        //This state is set in GroundCheck.
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Primed:
                    {
                        //This state is set in Jump.
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Airborne:
                    {
                        //This state is set in PlayerGravity.
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Tailwhip:
                    {
                        //The rest of these states are set in TrickManager.
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Barspin:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.One_Eighty_Fakie:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Clamp_Touch:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Finger_Whip:
                    {
                        animator.speed = .8f;
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Tailwhip_Rewind:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.ThreeSixty:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.T_Bog:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Truck_Driver:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.ThreeSixty_Tailwhip:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Scooter_Flip:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Lean_Left:
                    {
                        //This is set in HorizontalMovement
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Lean_Right:
                    {
                        //This is set in HorizontalMovement
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Grinding:
                    {
                        //This is set in RailObjectMover
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Ragdoll:
                    {
                        //This is set in HorizontalMovement
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Primed_Left:
                    {
                        //This is set in HorizontalMovement
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Primed_Right:
                    {
                        //This is set in HorizontalMovement
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.BarTwist:
                    {
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.TuckNoHander:
                    {
                        animator.speed = .8f;
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
                case PlayerState.Manual:
                    {
                        animator.speed = 1.2f;
                        animator.SetInteger("PlayerState", (int)state);
                        currentState = state;
                        doingTrick = false;
                        manual = true;
                        length = resetValue;
                        Debug.Log("The player is " + state);
                        break;
                    }
            }
    }

    void CheckIfTrickOver()
    {
        float clipLength = animator.GetCurrentAnimatorStateInfo(0).length;
        //print(length + " : " + clipLength);
        if (length >= clipLength)
        {
            if (manual)
            {
                comboManager.newComboValue = 2;
                comboManager.AddToCombo();
            }
            trickManager.doingTrick = false;
            trickManager_Mobile.doingTrick = false;
            doingTrick = false;
            manual = false;
            animator.speed = 1;
            if (currentState != PlayerStateManager.PlayerState.Pushing)
            {
                ActiveState(PlayerStateManager.PlayerState.Airborne);
            }
            if (currentState == PlayerStateManager.PlayerState.Manual)
            {
                ActiveState(PlayerStateManager.PlayerState.Pushing);
            }
        }

    }
}