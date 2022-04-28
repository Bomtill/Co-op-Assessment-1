using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour 
{
    public Canvas loadGameMenu;
    public Canvas settingsMenu;
    public Canvas mainMenu;

    public GameObject levelOneButton;
    public GameObject levelTwoButton;
    public GameObject levelThreeButton;

    public TMP_Text levelOneScore;
    public TMP_Text levelOnePlus;
    public TMP_Text levelTwoScore;
    public TMP_Text levelTwoPlus;
    public TMP_Text levelThreeScore;
    public TMP_Text levelThreePlus;


    private void Start()
    {
        Cursor.visible = true;
        settingsMenu.enabled = false;
        loadGameMenu.enabled = false;
        levelOneButton.SetActive(false);
        levelTwoButton.SetActive(false);
        levelThreeButton.SetActive(false);

    }
    public void NewGame()
    {
        MySceneManager.MSMInstance.LoadNewScene(1); // lobby scene
    }
    public void LevelSelect()
    {
        mainMenu.enabled = false;
        loadGameMenu.enabled = true;
        GameManager.GMInstance.LoadSaveGame();
        if(GameData.levelUnlocked >= 0) levelOneButton.SetActive(true);
        if (GameData.levelUnlocked >= 1) levelTwoButton.SetActive(true);
        if (GameData.levelUnlocked >= 2) levelThreeButton.SetActive(true);
        levelOneScore.SetText(GameData.levelOneScore);
        levelOnePlus.SetText(GameData.levelOnePlus);
        levelTwoScore.SetText(GameData.levelTwoScore);
        levelTwoPlus.SetText(GameData.levelTwoPlus);
        levelThreeScore.SetText(GameData.levelThreeScore);
        levelThreePlus.SetText(GameData.levelThreePlus);
    }

    public void LoadGameButton()
    {
        GameManager.GMInstance.LoadSaveGame();
    }
    public void LoadGameMenu()
    {
        mainMenu.enabled = false;
        loadGameMenu.enabled = true;
    }


    public void SettingsButton()
    {
        mainMenu.enabled = false;
        settingsMenu.enabled = true;
    }


    public void BackButton()
    {
        settingsMenu.enabled = false;
        loadGameMenu.enabled = false;
        mainMenu.enabled = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LevelOneButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(2);
    }
    public void LevelTwoButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(4);
    }
    public void LevelThreeButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(5);
    }
}
