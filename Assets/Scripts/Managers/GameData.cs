using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
    public int score = 0;
    public int scene = 0;
    public int levelUnlocked = 0;
    // the string is the key, the int could be the score
    // could be used for saving the time and scores of each level?
    public Dictionary<string, int> playerScores = new Dictionary<string, int>();

    // save players names
    // save a rating for each level
    // save scores for each level
    public static string playerOneNameData;
    public static string playerTwoNameData;

    public void AddScore(int points)
    {
        score += points;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public void UnlockLevel()
    {
        
        // get current scene int and plus 1
        levelUnlocked ++;
    }
}
