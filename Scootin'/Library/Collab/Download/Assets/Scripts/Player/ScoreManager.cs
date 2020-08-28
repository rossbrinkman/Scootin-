using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private int TotalScore;

    private ComboManager comboManager;
    private DistanceManager distanceManager;
    private CoinManager coinManager;

    //((distance traveled * 3) * (multiplier)) + (Coins* 4)
    private void Awake()
    {
        distanceManager = GetComponent<DistanceManager>();
        comboManager = GetComponent<ComboManager>();
        coinManager = GetComponent<CoinManager>();
    }

    private void Start()
    {
        TotalScore = 0;
        score = 0;
        UpdateScore();
    }

    private void FixedUpdate()
    {
        AddScore(TotalScore);

        TotalScore = distanceManager.itotalDistance + (coinManager.coins * 4);
    }


    public void AddScore (int newScoreValue)
    {
        //Debug.Log("Calling");
        newScoreValue = TotalScore;

        if (comboManager.combo == 1)
        {
            score = newScoreValue;
        }

        else if (comboManager.combo > 1)
        {
            score += newScoreValue * comboManager.combo;
        }

        //score = (distanceManager.itotalDistance * comboManager.combo + (coinManager.coins * 4));

        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}