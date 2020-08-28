using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManagers : MonoBehaviour
{
    public ParticleSystem ps1, ps2;
    public GameObject trail;
    public AudioManager audioManager;
    //public AudioManager audioManager;

    private PlayerStateManager stateManager;
    private Death death;
    private GroundCheck groundCheck;
    private AudioSource grindingaudioSource;
    private AudioSource audioSource;

    private bool grindingNoise = false;

    private bool jumpFallSound = false;


    // Start is called before the first frame update
    void Start()
    {
        death = GetComponent<Death>();
        stateManager = GetComponent<PlayerStateManager>();
        groundCheck = GetComponentInChildren<GroundCheck>();
        grindingaudioSource = ps1.GetComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();

        ps1.Stop(); ps2.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (stateManager.currentState == PlayerStateManager.PlayerState.Grinding && !death.dead)
        {
            if (!grindingNoise)
            {
                grindingaudioSource.Play();
                ps1.Play(); ps2.Play();
                grindingNoise = true;
            }
        }
        else
        {
            ps1.Stop(); ps2.Stop(); grindingaudioSource.Stop(); grindingNoise = false;
        }

        if (groundCheck.isGrounded && !death.dead)
        {
            trail.SetActive(true);
            //jumpUpSound = false;
            if (!jumpFallSound)
            {
                audioSource.clip = audioManager.clips[1];
                audioSource.Play();
                jumpFallSound = true;
            }
        }
        else
        {
            trail.SetActive(false);
            jumpFallSound = false;            
        }
    }
}
