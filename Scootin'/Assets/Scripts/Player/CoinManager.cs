using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public int coins;
    public AudioManager audioManager;
    private AudioSource audioSource;

    private Death death;

    private ComboManager comboManager;

    private void Awake()
    {
        audioSource = transform.parent.GetComponent<AudioSource>();
        death = GetComponent<Death>();
        comboManager = GetComponent<ComboManager>();
    }

    private void Start()
    {
        coins = 0;
        UpdateCoins();
    }

    public void AddCoins(int newCoinValue)
    {
        if (!death.dead)
        {
            coins += newCoinValue * comboManager.combo;
            UpdateCoins();
            audioSource.Play();
        }
    }

    void UpdateCoins()
    {
        coinText.text = "Coins: " + coins;
        PlayerPrefs.SetInt("AddCoins", coins);
    }
}

