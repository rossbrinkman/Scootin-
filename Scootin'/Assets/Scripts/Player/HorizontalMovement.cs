using UnityEngine;

//By Seth
//This script is responsible for moving the player side-to-side.
public class HorizontalMovement : MonoBehaviour
{
    [Tooltip("How high you want to jump.")]
    [SerializeField]
    private float speed = 10f;

    private CharacterController controller;
    private PlayerStateManager stateManager;

    [HideInInspector]
    public bool leaning = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        stateManager = GetComponent<PlayerStateManager>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        HoriMovement(x);

        Lean(x);
    }

    private void HoriMovement(float x)
    {
        //CharacterController.Move requires a Vector3.
        Vector3 move = transform.right * x;

        controller.Move(move * speed * Time.deltaTime);
    }

    private void Lean(float x)
    {
        if (stateManager.currentState != PlayerStateManager.PlayerState.Airborne && x!=0)
        {
            if (stateManager.currentState == PlayerStateManager.PlayerState.Pushing)
            {
                if (x > 0)
                {
                    leaning = true;
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Lean_Right);
                }

                if (x < 0)
                {
                    leaning = true;
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Lean_Left);
                }
            }
            else if (stateManager.currentState == PlayerStateManager.PlayerState.Primed)
            {
                if (x > 0)
                {
                    leaning = true;
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Primed_Right);
                }

                if (x < 0)
                {
                    leaning = true;
                    stateManager.ActiveState(PlayerStateManager.PlayerState.Primed_Left);
                }
            }
        }
        else if(x==0)
            leaning = false;
    }
}