using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsPageMenu : MonoBehaviour
{
    TMP_Text lastresult; // = ScoreManager.levelResult;
    TMP_Text plusMinusActive;  // = ScoreManager.Instance.plusMinus;

    private void Awake()
    {
        Cursor.visible = true;
    }
    public void SaveGameButton()
    {
        GameManager.GMInstance.SaveGame();
    }
    public void NextLevelButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(4); // second level
    }
    public void BackButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(0);
    }
}
