using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultsPageMenu : MonoBehaviour
{
    public TMP_Text lastresult; // = ScoreManager.levelResult;
    public TMP_Text plusMinusActive;  // = ScoreManager.Instance.plusMinus;

    private void Awake()
    {
        Cursor.visible = true;
        lastresult.SetText(ScoreManager.Instance.levelScore);
        plusMinusActive.SetText(ScoreManager.Instance.plusMinus);
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
