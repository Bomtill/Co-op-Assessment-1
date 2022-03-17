using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private static MySceneManager localInstance;
    public static MySceneManager Instance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("Scene manager instance is null!");
            }
            return localInstance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
