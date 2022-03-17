using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //set up singleton
    
    private static GameManager localInstance;
    public static GameManager Instance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("GameManager instance is null!");
            }return localInstance;
        }
    }

    bool isGameOver = false;

    //player spawn points? 
    //reset level
    //Timer for level?

    private void Awake()
    {
        DontDestroyOnLoad(this);
        localInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GameOver(bool flag)
    {
        isGameOver = flag;
    }

}
