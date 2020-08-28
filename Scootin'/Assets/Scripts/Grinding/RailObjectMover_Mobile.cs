using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailObjectMover_Mobile : MonoBehaviour
{
    [Range(1f, 10f)]
    public float railBraking = 2f;

    [HideInInspector]
    public Rail rail;
    [HideInInspector]
    public float speed = 2.5f;
    public MoverPlayMode mode;

    private int currentSegment;
    private float transition;
    [HideInInspector]
    public bool isCompleted = true;

    private ComboManager comboManager;
    //private ForwardMovement forwardMovement;
    //private HorizontalMovement_Mobile horizontalMovement;
    private Movement_Mobile movement;
    private PlayerStateManager stateManager;

    [HideInInspector]
    public bool start = false;

    Quaternion initialRotation;

    private void Start()
    {
        //forwardMovement = GetComponent<ForwardMovement>();
        //horizontalMovement = GetComponent<HorizontalMovement_Mobile>();
        comboManager = GetComponent<ComboManager>();
        movement = GetComponent<Movement_Mobile>();
        stateManager = GetComponent<PlayerStateManager>();

        initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (!rail)
            return;

        if (start)
            if (!isCompleted)
                Play();
    }

    public void Play()
    {
        movement.enabled = false;
        //horizontalMovement.enabled = false;

        if(stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
            stateManager.ActiveState(PlayerStateManager.PlayerState.Grinding);

        speed = movement.speed / railBraking;

        float magnitude = (rail.nodes[currentSegment + 1].position - rail.nodes[currentSegment].position).magnitude;
        float normalizedSpeed = (Time.deltaTime * 1 / magnitude) * speed;

        transition += normalizedSpeed;
        if(transition > 1)
        {
            transition = 0;
            currentSegment++;
            if(currentSegment == rail.nodes.Length - 1)
            {
                isCompleted = true;
                movement.enabled = true;
                //horizontalMovement.enabled = true;
                stateManager.ActiveState(PlayerStateManager.PlayerState.Airborne);
                transform.rotation = initialRotation;
                start = false;
                currentSegment = 0;
                comboManager.newComboValue = 1;
                comboManager.AddToCombo();
                return;
            }
        }
        else if(transition < 0)
        {
            transition = 1;
            currentSegment--;
        }

        transform.position = rail.PositionOnRail(currentSegment, transition, mode);
        //transform.rotation = rail.Orientation(currentSegment, transition);
    }
}
