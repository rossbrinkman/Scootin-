using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This script handles the trigger conditions for the tutorial screens -G$
public class DialogLogic : MonoBehaviour
{

    //allows you to edit what the tutorial windows in the editor
    public string popUp;
    public string popUp1;
    public string popUp2;
    public string popUp3;
    public string winText;
    public string grindText;
    public string tricksText;

    //this makes sure that each tutorial window is only called once in the scene.
    private static bool tutStart = false;
    private static bool controls = false;
    private static bool firstCombo = false;
    private static bool firstCoin = false;
    private static bool firstTrick = false;
    private static bool firstGrind = false;
    

    private ComboManager comboManager;
    private CoinManager coinManager;
    private PlayerStateManager playerState;



    void Start()
    {
        firstCombo = true;
        firstCoin = true;
        firstTrick = true;
        firstGrind = true;
        tutStart = true;
        controls = true;


        comboManager = FindObjectOfType<ComboManager>();
        coinManager = FindObjectOfType<CoinManager>();
        playerState = FindObjectOfType<PlayerStateManager>();
    }


    void Update()
    {
        TutDialog pop = GetComponent<TutDialog>();
        
        if(tutStart == true)
        {
            pop.PopUp(popUp);
            tutStart = false;
        }

        if (Time.time >= 3f && Time.time <= 3.1)
        {
            if (controls == true)
            {
                Debug.Log("Controls");
                pop.PopUp1(popUp1);

                controls = false;
            }
        }

        else if(coinManager.coins >= 1)
        {
            if(firstCoin == true)
            {
                pop.PopUp3(popUp3);
                firstCoin = false;
            }
        }


        else if(comboManager.combo >= 2)
        {
            if(firstCombo == true)
            {
                pop.PopUp2(popUp2);
                firstCombo = false;
            }
        }

        if (playerState.currentState == PlayerStateManager.PlayerState.Primed && firstTrick == true)
        {
            pop.TricksText(tricksText);
            firstTrick = false;
        }

        else if (playerState.currentState == PlayerStateManager.PlayerState.Grinding && firstGrind == true)
        {
                pop.GrindText(grindText);
                firstGrind = false;
        }

        else if (Time.time >= 60f)
        {
            Debug.Log("Win");
            pop.WinText(winText);
        }
    }
}
