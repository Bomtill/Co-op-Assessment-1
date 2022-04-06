using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour 
{
    public Canvas levelSelect;
    public Canvas settingsMenu;
    public Canvas mainMenu;

    private void Start()
    {
        
        Cursor.visible = true;
        settingsMenu.enabled = false;
        levelSelect.enabled = false;
    }
    public void NewGame()
    {
        MySceneManager.MSMInstance.LoadNewScene(1); // lobby scene
    }
    public void LevelSelect()
    {
        mainMenu.enabled = false;
        levelSelect.enabled = true;
        // load unlocked scenes from Save System
        // call the appropriate scene from MySceneManager
    }
    public void SettingsButton()
    {
        mainMenu.enabled = false;
        settingsMenu.enabled = true;
        // volume slider
        // see controls
    }


    public void BackButton()
    {
        settingsMenu.enabled = false;
        levelSelect.enabled = false;
        mainMenu.enabled = true;
    }
    public void QuitGame()
    {
        Application.Quit();
    }


}
