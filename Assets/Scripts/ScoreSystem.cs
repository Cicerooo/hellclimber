using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreSystem
{
    public static void SetHighScore()
    {
        if (PlayerPrefs.GetFloat("Score")>PlayerPrefs.GetFloat("HighScore"))
        {
            PlayerPrefs.SetFloat("HighScore", PlayerPrefs.GetFloat("Score"));
        }
    }
    public static float GetHighScore()
    {
        return PlayerPrefs.GetFloat("HighScore");
    }

    public static float GetCurrentScore()
    {
        return PlayerPrefs.GetFloat("Score");
    }

    public static void AddScore(int circle, float time=0, float deaths=0, float score=0)
    {
        
    }
}
