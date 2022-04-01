using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject endLevelCube;
    public GameObject keyCard;
    //timer for level
    //Score counter
    // int for amount of times spotted for score
    bool gamePaused = false;
    private float sceneTimer;

    public static bool keyPickedUp = false;

    public Canvas gameOverScreen;
    public Canvas menuScreen;
    public Canvas hintOverlay;
    void Start()
    {
        gameOverScreen.enabled = false;
        menuScreen.enabled = false;
        hintOverlay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyPickedUp) keyCard.SetActive(false);
        sceneTimer = Time.timeSinceLevelLoad;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!gamePaused)
            {
                
                menuScreen.enabled = true;
                gamePaused = true;
            } else
            {
                menuScreen.enabled = false;
                gamePaused = false;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // have it fade in
                hintOverlay.enabled = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                hintOverlay.enabled = false;
            }
        }
    }
    public void SaveGame()
    {
        GameManager.GMInstance.SaveGame();
    }
    private void EnableGameOverScreen()
    {
        gameOverScreen.enabled = true;
    }
    public void RestartLevelButton()
    {
        MySceneManager.MSMInstance.ReloadCurrentScene();
    }

    public void ExitToMainButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(0);
    }

    
    public void GameOver()
    {
        GameManager.GMInstance.FadeEffect();
        Invoke("EnableGameOverScreen", 1.5f);
        // do game over thing
    }

    public void LevelFinished()
    {
        MySceneManager.MSMInstance.LoadNewScene(0); // return to main
        GameManager.GMInstance.UnlockNextLevel();
    }

    
    private void OnEnable()
    {
        PatrolEnemy.GameOverEvent += GameOver;
        EndLevelTrigger.FinishedLevelEvent += LevelFinished;
    }
    private void OnDisable()
    {
        PatrolEnemy.GameOverEvent -= GameOver;
        EndLevelTrigger.FinishedLevelEvent -= LevelFinished;
    }
}
