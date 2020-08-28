using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private ComboManager comboManager;
    private DistanceManager distanceManager;
    private CoinManager coinManager;

    private int score;
    private int coins;
    private int distance;
    private bool coinChanged;
    private bool distanceChanged;

    //((distance traveled * 3) * (multiplier)) + (Coins* 4)
    private void Awake()
    {
        distanceManager = GetComponent<DistanceManager>();
        comboManager = GetComponent<ComboManager>();
        coinManager = GetComponent<CoinManager>();
    }

    private void Start()
    {
        distance = distanceManager.itotalDistance;
        coins = coinManager.coins;
        coinChanged = false;
        distanceChanged = false;
        score = 0;
        
        UpdateScore();
    }

    private void FixedUpdate()
    {
        if (coins != coinManager.coins) { coinChanged = true; }
        if (distance != distanceManager.itotalDistance) { distanceChanged = true; }

        if (distanceChanged)
        {
            int change = distanceManager.itotalDistance - distance;
            score += (change * comboManager.combo);
            distanceChanged = false;
            distance = distanceManager.itotalDistance;
        }

        if (coinChanged)
        {
            int change = coinManager.coins - coins;
            score += (change * 4);
            coinChanged = false;
            coins = coinManager.coins;
        }

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}