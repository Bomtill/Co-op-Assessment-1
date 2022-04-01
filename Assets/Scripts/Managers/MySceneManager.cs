using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private int levelToLoad;
    
    public static int currentScene;

    public static event Action SceneLoadEvent;


    #region Singleton
    private static MySceneManager localInstance;
    public static MySceneManager MSMInstance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("Scene manager instance is null!");
            }
            return localInstance;
        }
    }
    #endregion
    private void Awake()
    {
        
        DontDestroyOnLoad(this);
        localInstance = this;
    }
    
    public void LoadNewScene(int sceneIndex) // add some kind of timeer to it.
    {
        SceneManager.LoadScene(sceneIndex);
        SceneLoadEvent?.Invoke();
        
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneLoadEvent?.Invoke();
        
    }
   
    public void GetCurrentScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
