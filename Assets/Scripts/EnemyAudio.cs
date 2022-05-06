using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    public AudioClip footStep;
    public AudioClip gunShot;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void GunShotSFX()
    {
        audioSource.PlayOneShot(gunShot);
    }
    public void Step()
    {
        audioSource.PlayOneShot(footStep);
    }
}
