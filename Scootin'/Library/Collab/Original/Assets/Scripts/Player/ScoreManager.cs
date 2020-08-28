using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private int totalScore;

    private ComboManager comboManager;
    private DistanceManager distanceManager;
    private CoinManager coinManager;

    private int combo;
    private int prevScore;
    private bool changedSinceLastFrame = false;

    //((distance traveled * 3) * (multiplier)) + (Coins* 4)
    private void Awake()
    {
        distanceManager = GetComponent<DistanceManager>();
        comboManager = GetComponent<ComboManager>();
        coinManager = GetComponent<CoinManager>();
    }

    private void Start()
    {
        prevScore = 0;
        totalScore = 0;
        score = 0;
        combo = comboManager.combo;
        
        UpdateScore();
    }

    private void FixedUpdate()
    {
        if (comboManager.combo != combo)
        {
            print("change");
            changedSinceLastFrame = true;
            combo = comboManager.combo;
        }
        if (changedSinceLastFrame)
        {
            prevScore = totalScore;
            totalScore = 0;
            changedSinceLastFrame = false;
            distanceManager.oldPos = transform.position;
        }

        AddScore(totalScore);

        totalScore = distanceManager.itotalDistance * comboManager.combo + (coinManager.coins * 4);
    }


    public void AddScore (int newScoreValue)
    {
        score = prevScore + newScoreValue;

        //score = (distanceManager.itotalDistance * comboManager.combo + (coinManager.coins * 4));

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}