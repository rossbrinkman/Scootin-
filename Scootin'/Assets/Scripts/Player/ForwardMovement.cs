using UnityEngine;

//By Seth
//This script is responsible for accelerating the player forward perpendicular to the ground until they reach a max speed.
public class ForwardMovement : MonoBehaviour
{
    [SerializeField]
    private float initialSpeed = 0f;

    [SerializeField]
    private float maxSpeed = 30f;

    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float acceleration = 0.1f;

    [HideInInspector]
    public float speed;

    //A value that increases over time.
    private float t = 0f;

    private CharacterController controller;
    private GroundCheck groundCheck;
    private PlayerStateManager stateManager;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        groundCheck = GetComponent<GroundCheck>();
        stateManager = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        if (stateManager.currentState != PlayerStateManager.PlayerState.Grinding)
        {
            MoveForward();
        }
    }

    private void MoveForward()
    {
        //Gives a value between two numbers over t.
        float z = Mathf.Lerp(initialSpeed, maxSpeed, t);

        //Store our speed to be accessed for grinding
        speed = z;

        //Increase our acceleration over time.
        t += acceleration * Time.deltaTime;

        //CharacterController.Move requires a Vector3.
        Vector3 velocity = new Vector3(0, 0, z);

        //Find a rotation using the ground we last touched.
        Quaternion rotation = Quaternion.FromToRotation(transform.up, groundCheck.groundNormal);

        //"Rotate" our movement vector.
        Vector3 move = rotation * velocity;

        //Apply this to the character controller component.
        controller.Move(move * Time.deltaTime);
    }
}
