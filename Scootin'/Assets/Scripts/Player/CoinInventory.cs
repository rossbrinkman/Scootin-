using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


[Serializable]
public class CoinInventory : MonoBehaviour
{
    private int totalCoins;
    private int coinsAdded;

    public TextMeshProUGUI coinText;


    private void Start()
    {
        totalCoins = PlayerPrefs.GetInt("TotalCoins");
        coinsAdded = PlayerPrefs.GetInt("AddCoins", 0);
        UpdateTotal(coinsAdded);
    }



    public void UpdateTotal(int newCoinValue)
    {
        totalCoins += newCoinValue;
        PlayerPrefs.SetInt("TotalCoins", totalCoins);
        PlayerPrefs.DeleteKey("AddCoins");
        UpdateHUD();
    }

    void UpdateHUD()
    {
        coinText.text = "Total Coins: " + totalCoins;
    }
}
