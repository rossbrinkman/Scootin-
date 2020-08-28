using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles the coin "pick up", destruction and adds points to the score. -G$
public class PickUp : MonoBehaviour
{
    public GameObject Coin;

    private CoinManager coinManager;

    private void Awake()
    {
        //we get the ScoreManager script from the Player gameobject
        coinManager = GameObject.FindGameObjectWithTag("Player").GetComponent<CoinManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //we add 1 points to the total score
            coinManager.AddCoins(1);
            //and destroy the game object
            Destroy(Coin);
        }
    }   
}
