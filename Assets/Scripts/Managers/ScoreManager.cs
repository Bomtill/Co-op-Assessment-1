using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // get score from level manager
    //public static int score;
    public string levelScore;
    public string plusMinus;
    public static int playerSeenCount = 0;


    #region Singleton
    private static ScoreManager localInstance;
    public static ScoreManager Instance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("ScoreManager instance is null");
            }return localInstance;
        }
    }
    #endregion
    // Start is called before the first frame update
    private void Awake()
    {
        localInstance = this;
    }

    public void SetScore(int scoreTime)
    {
        if (scoreTime < 1) levelScore = "S";
        if (scoreTime == 1) levelScore = "A";
        if (scoreTime == 2) levelScore = "B";
        if (scoreTime == 3) levelScore = "C";
        if (scoreTime > 3) levelScore = "D";
    }
    void PlusActive(int seenCount)
    {
        if (seenCount < 1) plusMinus = "++";
        if (seenCount == 1) plusMinus = "+";
        if (seenCount >= 2 && seenCount <= 4) plusMinus = ""; // no plus or minus
        if (seenCount > 4) plusMinus = "-";
    }

}
