using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource backgroundMusic;

    public AudioSource[] sfx;

    //before start
    private void Awake()
    {
        instance = this;
    }


    public void PlaySFX(int sfxNumber)
    {

        //Stoping and playing it to not interact with anything while triggering
        sfx[sfxNumber].Stop();
        sfx[sfxNumber].Play();
    }

    public void StopBackgroundMusic()
    {
        backgroundMusic.Stop();
    }
}
