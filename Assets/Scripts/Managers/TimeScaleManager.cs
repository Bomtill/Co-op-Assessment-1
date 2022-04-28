using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleManager : MonoBehaviour
{

    private static TimeScaleManager localInstance;
    public static TimeScaleManager TSMInstance
    {
        get {
            if (localInstance == null)
            {
                Debug.LogError("Time Scale Manager instance is null!");
            }
            return localInstance;
        }
    }

    public static float timeScale = 1;

    public bool canSlowTime = true;
    public bool canStopTime = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
