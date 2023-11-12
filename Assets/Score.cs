using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    [SerializeField] private TextMeshProUGUI scoreTmp;
    public int currentScore = 0;
    private void Awake()
    {
        instance = this;
    }
    public void AddScore(int scoreAmount)
    {
        currentScore += scoreAmount;
        UpdateScoreVisual();
    }
    private void UpdateScoreVisual()
    {
        scoreTmp.text = currentScore.ToString();
    }
}
