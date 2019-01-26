using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static ScoreController instance;
    public int score = 0;

    void OnEnable()
    {
        instance = this;
    }

    public static void AddScore(int score)
    {
        instance.score = instance.score + score;
    }

    void Update()
    {
        UI.instance.scoreText.text = "Score: " + score;
    }
}
