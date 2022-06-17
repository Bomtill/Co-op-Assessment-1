using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject endLevelCube;
    //public GameObject keyCard;
    bool gamePaused = false;
    private float sceneTimer;
    NetworkManager networkManager;

    
    public int scoreTimer;
    public static bool keyPickedUp = false;

    public MeshRenderer exitArrow;
    public Canvas gameOverScreen;
    public Canvas menuScreen;
    public Canvas hintOverlay;

    
    void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        ScoreManager.playerSeenCount = 0;
        Cursor.visible = false;
        gameOverScreen.enabled = false;
        menuScreen.enabled = false;
        hintOverlay.enabled = false;
        StartCoroutine(LevelScoreTimer());
        exitArrow.enabled = false;
        GameManager.GMInstance.SaveGame();
    }

    // Update is called once per frame
    void Update()
    {
        //if (keyPickedUp) keyCard.SetActive(false);
        sceneTimer = Time.timeSinceLevelLoad;

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!gamePaused)
            {
                //Time.timeScale = 0;
                Cursor.visible = true;
                menuScreen.enabled = true;
                gamePaused = true;
            } else
            {
                //Time.timeScale = 1;
                Cursor.visible = false;
                menuScreen.enabled = false;
                gamePaused = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            //Debug.Log("Spacebar pressed");
            hintOverlay.enabled = true;
            exitArrow.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            hintOverlay.enabled = false;
            exitArrow.enabled = false;
        }
    }
    public void SaveGame()
    {
        GameManager.GMInstance.SaveGame();
    }
    private void EnableGameOverScreen()
    {
        gameOverScreen.enabled = true;
        GameManager.GMInstance.FadeIn();
        Cursor.visible = true;
    }
    public void RestartLevelButton()
    {
        networkManager.Restart();
        gameOverScreen.enabled = false;
        GameManager.GMInstance.FadeEffect();
        Cursor.visible = false;

        //MySceneManager.MSMInstance.ReloadCurrentScene();
    }

    public void ExitToMainButton()
    {
        MySceneManager.MSMInstance.LoadNewScene(0);
    }

    
    public void GameOver()
    {
        GameManager.GMInstance.FadeEffect();
        Invoke("EnableGameOverScreen", 1.5f);
        //Invoke("StopGameAfterGameOver", 2.5f);
    }

    public void LevelFinished()
    {
        ScoreManager.Instance.PlusActive(ScoreManager.playerSeenCount);
        ScoreManager.Instance.SetScore(scoreTimer);
        MySceneManager.MSMInstance.LoadNewScene(3); // score results page
        GameManager.GMInstance.UnlockNextLevel();
        
    }
    private void StopGameAfterGameOver()
    {
        Time.timeScale = 0;
    }
    private IEnumerator LevelScoreTimer()
    {
        yield return new WaitForSeconds(45);
        scoreTimer++;
        yield return new WaitForSeconds(45);
        scoreTimer++;
        yield return new WaitForSeconds(60);
        scoreTimer++;
        yield return new WaitForSeconds(90);
        scoreTimer++;
        yield return null;
    }
    private void KeyCardPickedUp()
    {
        keyPickedUp = true;
    }
    
    
    private void OnEnable()
    {
        KeyCard.KeyCardPickedUp += KeyCardPickedUp;
        PatrolEnemy.GameOverEvent += GameOver;
        EndLevelTrigger.FinishedLevelEvent += LevelFinished;
    }
    private void OnDisable()
    {
        KeyCard.KeyCardPickedUp -= KeyCardPickedUp;
        PatrolEnemy.GameOverEvent -= GameOver;
        EndLevelTrigger.FinishedLevelEvent -= LevelFinished;
    }
}
