using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Animator fadeAnimator;
    //set up singleton
    #region Singleton
    private static GameManager localInstance;
    public static GameManager GMInstance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("GameManager instance is null!");
            }return localInstance;
        }
    }
    #endregion
    GameObject levelManager;
    GameData gameData = new GameData();

    private void Awake()
    {
        DontDestroyOnLoad(this);
        localInstance = this;
        fadeAnimator = GameObject.Find("FadeCanvas").GetComponentInChildren<Animator>();
    }

    
    public void LoadSaveGame()
    {
        gameData = SaveSystem.instance.LoadGame();
        Debug.Log("Loaded game");
    }
    public void SaveGame()
    {
        SaveSystem.instance.SaveGame(gameData);
        Debug.Log("Saved Game");
        Debug.Log(GameData.playerOneNameData);
        Debug.Log(GameData.playerTwoNameData);
    }
    public void UnlockNextLevel()
    {
        gameData.UnlockLevel();
    }
    public void SceneLoaded()
    {
        fadeAnimator = GameObject.Find("FadeCanvas").GetComponentInChildren<Animator>();
        levelManager = GameObject.Find("LevelManager");
        Invoke("FadeEffect", 1f);
        Time.timeScale = 1;
        if (levelManager != null)
        {
            ScoreManager.playerSeenCount = 0;
            LevelManager.keyPickedUp = false;
        }
    }

    public void UpdatePlayerOneName(string name1)
    {
        GameData.playerOneNameData = name1;
    }
    public void UpdatePlayerTwoName(string name2)
    {
        GameData.playerTwoNameData = name2;
    }
    public void FadeIn() 
    {
        fadeAnimator.SetTrigger("FadeIn");
    }
    public void FadeEffect()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }
    private void OnEnable()
    {
        MySceneManager.SceneLoadEvent += SceneLoaded;
    }
    private void OnDisable()
    {
        MySceneManager.SceneLoadEvent -= SceneLoaded;
    }

}
