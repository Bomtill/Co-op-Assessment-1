using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public static string playerOneNameData;
    public static string playerTwoNameData;
    public static int levelUnlocked = 0;
    public static bool levelOneClear = false;
    public static string levelOneScore;
    public static string levelOnePlus;
    public static bool levelTwoClear = false;
    public static string levelTwoScore;
    public static string levelTwoPlus;
    public static bool levelThreeClear = false;
    public static string levelThreeScore;
    public static string levelThreePlus;

    // the string is the key, the int could be the score
    // could be used for saving the time and scores of each level?
    public Dictionary<string, int> playerScores = new Dictionary<string, int>();

    public void UnlockLevel()
    {
        levelUnlocked ++;
        if (levelUnlocked == 1)
        {
            levelOneClear = true;
            levelOneScore = ScoreManager.Instance.levelScore;
            levelOnePlus = ScoreManager.Instance.plusMinus;
        }
        if (levelUnlocked == 2)
        {
            levelTwoClear = true;
            levelTwoScore = ScoreManager.Instance.levelScore;
            levelTwoPlus = ScoreManager.Instance.plusMinus;
        }
        if(levelUnlocked == 3)
        {
            levelThreeClear = true;
            levelThreeScore = ScoreManager.Instance.levelScore;
            levelThreePlus = ScoreManager.Instance.plusMinus;
        }
    }
}
