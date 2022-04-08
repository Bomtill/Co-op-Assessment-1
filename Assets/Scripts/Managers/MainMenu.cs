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

    private void Start()
    {
        Cursor.visible = true;
        settingsMenu.enabled = false;
        loadGameMenu.enabled = false;
    }
    public void NewGame()
    {
        MySceneManager.MSMInstance.LoadNewScene(1); // lobby scene
    }
    public void LevelSelect()
    {
        // load unlocked scenes from Save System
        // call the appropriate scene from MySceneManager
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


}
