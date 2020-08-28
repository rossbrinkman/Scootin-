using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

//By Seth
//
//A Trick is a class that holds a lot of information about each individual Trick. This information is set in the inspector.
//
//A TrickStep is one of the steps that is required to complete a trick.

//A list of types of TrickSteps. Up = 0, Down = 1, etc.
public enum TrickStepType { up = 0, down = 1, left = 2, right = 3, touch = 4 }

public class TrickManager : MonoBehaviour
{
    private PlayerStateManager stateManager;
    private ComboManager comboManager;

    [Tooltip("How long you have to complete a trick.")]
    public float trickLeeway = 0.5f;
    private float currentLeeway = 0;

    [Tooltip("This lets us assign variables to keys easily in the inspector.")]
    [Header("Inputs")]
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode spaceKey;

    [Tooltip("These are all of the possible different steps a trick can have. The inputs.")]
    [Header("Trick Steps")]
    public TrickStep upStep;
    public TrickStep downStep;
    public TrickStep leftStep;
    public TrickStep rightStep;
    public TrickStep touchStep;

    [Tooltip("A list of all of our Tricks which you can edit here in the inspector.")]
    [Header("Tricks")]
    public List<Trick> tricks;

    private TrickStep currentTrickStep = null;
    private TrickInput lastInput = null;
    [HideInInspector]
    public bool doingTrick = false;

    //A list of the tricks that are currently possible to complete given the player's recent inputs.
    private List<int> currentTricks = new List<int>();

    //private bool skip = false;

    //To store the most recently performed trick with the longest input.
    TrickStep performedTrickStep = null;
    Trick performedTrick = null;

    private void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();
        comboManager = GetComponent<ComboManager>();

        //We need to start listening for Tricks and take some actions if one is completed.
        PrimeTricks();
    }

    private void PrimeTricks()
    {
        //For every trick listed in the inspector
        for (int i = 0; i < tricks.Count; i++)
        {
            //Assign that given trick to "trick".
            Trick trick = tricks[i];

            //Wait and listen for that trick to be completed. 
            trick.onInputted.AddListener(() =>
            {
                //When a trick is fully inputted, we want to do these things.

                //skip = true;

                if(performedTrick != null)
                {
                    if (trick.inputs.Count > performedTrick.inputs.Count)
                    {
                        //Save that trick for later use.
                        performedTrickStep = trick.trickStep;
                        performedTrick = trick;
                    }
                }
                else
                {
                    performedTrickStep = trick.trickStep;
                    performedTrick = trick;
                }
            });
        }
    }

    private void Update()
    {
        //Debug.Log("currentTricks.Count = " + currentTricks.Count);

        //If there are any tricks that can possibly be done given the player's recent inputs,
        if (currentTricks.Count > 0)
        {
            currentLeeway += Time.deltaTime;

            //When the time to enter combos runs out,
            if (currentLeeway >= trickLeeway)
            {
                if (lastInput != null)
                {
                    if(performedTrickStep != null)
                    {
                        //Perform the last saved trick.
                        TrickStep(performedTrickStep);
                    }
                    performedTrickStep = null;
                    performedTrick = null;
                    lastInput = null;
                }
                ResetTricks();
            }
        }
        else
            currentLeeway = 0;

        //Clear last frame's input.
        TrickInput input = null;

        //This is where we look for input.
        //
        //Currently this does not allow for multiple tricks in the same jump. I don't think we want that.
        if(stateManager.currentState == PlayerStateManager.PlayerState.Airborne)
        {
            if (Input.GetKeyDown(upKey))
                input = new TrickInput(TrickStepType.up);
            if (Input.GetKeyDown(downKey))
                input = new TrickInput(TrickStepType.down);
            if (Input.GetKeyDown(leftKey))
                input = new TrickInput(TrickStepType.left);
            if (Input.GetKeyDown(rightKey))
                input = new TrickInput(TrickStepType.right);
            if (Input.GetKeyDown(spaceKey))
                input = new TrickInput(TrickStepType.touch);
        }   

        //If the player did not input anything, exit update for this frame.
        if (input == null) return;

        //Store which input the player inputted.
        lastInput = input;

        //Create a list of tricks to remove.
        List<int> remove = new List<int>();
        //For every trick that is currently possible.
        for (int i = 0; i < currentTricks.Count; i++)
        {
            Trick t = tricks[currentTricks[i]];
            //Call the ContinueTrick method and if the input that was given this frame can complete this given trick.
            if (t.ContinueTrick(input))
            {
                //Give the player more time to input more TrickSteps
                currentLeeway = 0;
            }
            else
            {
                //If that input was not valid for a given trick, get ready to remove that trick from the list of possible tricks.
                remove.Add(i);
            }
        }

        //if (skip)
        //{
        //    skip = false;
        //    return;
        //}

        //For every trick that exists
        for (int i = 0; i < tricks.Count; i++)
        {
            //if that trick is already in the currentTricks list, continue the loop
            if (currentTricks.Contains(i)) continue;
            //if 
            if (tricks[i].ContinueTrick(input))
            {
                currentTricks.Add(i);
                currentLeeway = 0;
            }
        }

        //Remove all of the tricks that became invalid from the list of possible tricks.
        foreach (int i in remove)
        {
            if(remove.Count < currentTricks.Count)
            {
                if (i < currentTricks.Count)
                {
                    currentTricks.RemoveAt(i);
                }
            }
        } 
    }

    private void ResetTricks()
    {
        currentLeeway = 0;

        //For every trick that is currently possible.
        for (int i = 0; i < currentTricks.Count; i++)
        {
            Trick trick = tricks[currentTricks[i]];
            //Set that Trick's current input to 0.
            trick.ResetTrick();
        }

        //Clear the list of current possible Tricks.
        currentTricks.Clear();
    }

    //A method that activates a given trick
    public void TrickStep(TrickStep trickStep)
    {
        currentTrickStep = trickStep;

        if (currentTrickStep.state != PlayerStateManager.PlayerState.Idle && !doingTrick)
        {
            //Change the player's state to that trick.
            stateManager.ActiveState(currentTrickStep.state);
            doingTrick = true;
        }

        if (currentTrickStep.comboMultiplier != 0)
        {
            //Add to the player's multiplier.
            comboManager.newComboValue = currentTrickStep.comboMultiplier;
            comboManager.AddToCombo();
        }
    }

    private TrickStep getTrickStepFromType(TrickStepType t)
    {
        if (t == TrickStepType.up)
            return upStep;
        if (t == TrickStepType.down)
            return downStep;
        if (t == TrickStepType.left)
            return leftStep;
        if (t == TrickStepType.right)
            return rightStep;
        if (t == TrickStepType.touch)
            return touchStep;
        return null;
    }
}


[System.Serializable]
public class TrickStep
{
    public string name;
    public float length;

    [Tooltip("What state this would put the player in. Usually this is the name of the trick.")]
    public PlayerStateManager.PlayerState state;

    [Tooltip("How much this trick adds to our combo multiplier.")]
    [Range(0, 4)]
    public int comboMultiplier;
}

[System.Serializable]
public class TrickInput
{
    public TrickStepType type;

    public TrickInput(TrickStepType t)
    {
        type = t;
    }

    //A test that we use in the Trick class.
    public bool isSameAs(TrickInput test)
    {
        return (type == test.type);
    }
}

//This is a class that defines what a Trick is.
[System.Serializable]
public class Trick
{
    public TrickStep trickStep;

    [Tooltip("What inputs the trick requires to complete, in order.")]
    public List<TrickInput> inputs;

    [Tooltip("For tricks that can be completed on left OR right sides.")]
    public List<TrickInput> alternateInputs;

    //A unity event that occurs when this trick is activated.
    [HideInInspector]
    public UnityEvent onInputted;
    
    //An index that keeps track of what step we are on for this trick.
    int currentInput = 0;

    //A test to see if a given input lets you continue to try to perform this trick or makes you fail this trick.
    public bool ContinueTrick(TrickInput i)
    {
        if(alternateInputs.Count > 0)
        {
            //Starting with the first required input for this trick, if that matches the input the player pressed,
            if (inputs[currentInput].isSameAs(i) || alternateInputs[currentInput].isSameAs(i))
            {
                //Move onto the next required input.
                currentInput++;
                //If the player completed all the required inputs.
                if (currentInput >= inputs.Count || currentInput >= alternateInputs.Count)
                {
                    //Invoke this trick,
                    onInputted.Invoke();
                    //And reset the index,
                    currentInput = 0;
                }
                //And return true.
                return true;
            }
            //If that input does not match the required input,
            else
            {
                //Reset our index and return false.
                currentInput = 0;
                return false;
            }
        }
        else
        {
            //Starting with the first required input for this trick, if that matches the input the player pressed,
            if (inputs[currentInput].isSameAs(i))
            {
                //Move onto the next required input.
                currentInput++;
                //If the player completed all the required inputs.
                if (currentInput >= inputs.Count)
                {
                    //Invoke this trick,
                    onInputted.Invoke();
                    //And reset the index,
                    currentInput = 0;
                }
                //And return true.
                return true;
            }
            //If that input does not match the required input,
            else
            {
                //Reset our index and return false.
                currentInput = 0;
                return false;
            }
        }
    }

    public TrickInput currentTrickInput()
    {
        if (currentInput >= inputs.Count || currentInput >= alternateInputs.Count) return null;
        return inputs[currentInput];
    }

    //A method that resets our index.
    public void ResetTrick()
    {
        currentInput = 0;
    }
}