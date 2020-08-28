using UnityEngine;

public class Death : MonoBehaviour
{
    private ForwardMovement forwardMovement;
    private HorizontalMovement horizontalMovement;
    private HorizontalMovement_Mobile horizontalMovementMobile;
    private Movement movement;
    private Movement_Mobile movement_M;
    private GroundCheck groundCheck;
    private PlayerStateManager stateManager;
    private Ragdoll rag;
    private Jump jump;
    private Jump_Mobile jumpMobile;
    public CameraFollow cam;
    private Rigidbody scooter_rb;
    public GameObject trail;

    public AudioSource asource;

    public GameObject gameOverObject;
    public GameObject scooter;
    public Animator animator;
    private GameOver gameOver;
    [HideInInspector]
    public bool dead = false; 
    private bool canDie = true;
    private bool obsInFront = false;

    private void Start()
    {
        scooter_rb = scooter.GetComponent<Rigidbody>();
        rag = GetComponentInChildren<Ragdoll>();
        movement = GetComponent<Movement>();
        movement_M = GetComponent<Movement_Mobile>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        stateManager = GetComponent<PlayerStateManager>();
        forwardMovement = GetComponent<ForwardMovement>();
        horizontalMovement = GetComponent<HorizontalMovement>();
        horizontalMovementMobile = GetComponent<HorizontalMovement_Mobile>();
        jump = GetComponent<Jump>();
        jumpMobile = GetComponent<Jump_Mobile>();

        gameOver = gameOverObject.GetComponent<GameOver>();
    }

    void Update()
    {
        if(stateManager.currentState == PlayerStateManager.PlayerState.Ragdoll && canDie)
        {
            KillPlayer();
        }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1.5f) && hit.transform.tag == "Obstacle")
        {
            obsInFront = true;
        }
    }

    public void KillPlayer()
    {
        canDie = false;
        jump.enabled = false;
        jumpMobile.enabled = false;
        trail.SetActive(false);
        dead = true;
        asource.Play();
        //horizontalMovement.enabled = false;
        //horizontalMovementMobile.enabled = false;
        //forwardMovement.enabled = false;
        movement.enabled = false;
        movement_M.enabled = false;

        scooter_rb.isKinematic = false; scooter_rb.useGravity = true;
        //forwardMovement.speed = 0;
        cam.enabled = false;
        rag.Die();
    }

    private void RevivePlayer()
    {
        jump.enabled = true;
        jumpMobile.enabled = true;
        trail.SetActive(true);
        dead = false;
        canDie = true;
        //horizontalMovement.enabled = true;
        //horizontalMovementMobile.enabled = true;
        //forwardMovement.enabled = true;
        movement.enabled = true;
        movement_M.enabled = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //print(groundCheck.isOnObstacle + " " + dead + " " + canDie + " " + other.gameObject.name + " " + other.gameObject.tag);
        if (other.gameObject.CompareTag ("Obstacle") && (!groundCheck.isOnObstacle || obsInFront) && !dead && canDie)
        {
            Debug.Log("The player hit an obstacle.");
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            stateManager.ActiveState(PlayerStateManager.PlayerState.Ragdoll);
            KillPlayer();
            //GetComponentInChildren<TrailRenderer>().enabled = false;
            End();
        }
    }

    public void End()
    {
        gameOver.gameOver();
    }
}
