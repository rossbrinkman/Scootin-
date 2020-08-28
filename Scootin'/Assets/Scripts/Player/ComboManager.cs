using UnityEngine;
using TMPro;
using System.Collections;

public class ComboManager : MonoBehaviour
{
    public TextMeshProUGUI comboText;
    [Range(0, 20)]
    public int timeLimit;

    [HideInInspector]
    public int combo;
    [HideInInspector]
    public int newComboValue = 0;
    private int lastComboValue;
    private bool canWait = true;

    public int maxCombo;

    private PlayerStateManager stateManager;

    private void Start()
    {
        stateManager = GetComponent<PlayerStateManager>();

        combo = 1;
        lastComboValue = 1;
        UpdateComboText();
    }

    private void Update()
    {
        //print(newComboValue);
        //if my combo increases
        if(combo > lastComboValue)
        {
            StopCoroutine(ComboTimeout());
            //start a timer
            if(canWait)
                StartCoroutine(ComboTimeout());
        }

        lastComboValue = combo;
    }

    //Would be nice to have some functionality that hides the combo while it's set to 1x.

    public void AddToCombo()
    {
        combo += newComboValue;
        if (combo > maxCombo)
            combo = maxCombo;
        UpdateComboText();
    }

    void UpdateComboText()
    {
        comboText.text = "Combo : " + combo + "x";
    }

    public void ResetCombo()
    {
        combo = 1;
        UpdateComboText();
    }

    IEnumerator ComboTimeout()
    {
        //int timerComboValue = combo;
        canWait = false;
        //Debug.Log("timerComboValue = " + timerComboValue);

        yield return new WaitForSeconds(timeLimit);

        //if the timer runs out and I haven't increased my combo, reset my combo.
        //if (/*combo == timerComboValue && */combo > 1)
        //{
        canWait = true;
        combo -= 3;
        if (combo < 1)
        {
            combo = 1;
            UpdateComboText();
        }
        else
        {
            UpdateComboText();
            StartCoroutine(ComboTimeout());
        }
        //}
    }
}
