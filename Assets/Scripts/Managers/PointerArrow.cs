using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerArrow : MonoBehaviour
{
    Vector3 target;
    // Update is called once per frame
    private void Awake()
    {
        target = GameObject.Find("EndLevelCube").transform.position;
    }
    void Update()
    {
        transform.LookAt(target, Vector3.up);
    }
}
