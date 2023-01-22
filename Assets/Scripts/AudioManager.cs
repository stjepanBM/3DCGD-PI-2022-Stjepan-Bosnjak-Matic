using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusic;

    //before start
    private void Awake()
    {
        instance = this;
    }


    void Update()
    {
        
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}
