using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;


public class audioManager : MonoBehaviour
{

    public static audioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public AudioSource mainMenu, levelMusic, bossMusic;
    public AudioSource[] sfx;

    public void PlayMainMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        mainMenu.Play();
    }

    public void PlayLevelMusic()
    {
        if (!levelMusic.isPlaying)
        {
            bossMusic.Stop();
            mainMenu.Stop();
            levelMusic.Play();

        }
    }

    public void PlayBossMusic()
    {
        levelMusic.Stop();
        bossMusic.Play();
    }

    public void PlayAFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Stop();
        sfx[sfxToPlay].Play();
    }

    //public void PlaySFXAdjusted(int sfxToAdjust)
    //{
    //    System.Random random = new System.Random();
    //    sfx[sfxToAdjust].pitch = (float)random.NextDouble() * 0.4f + 0.8f;
    //    PlayAFX(sfxToAdjust);
    //}

    public void PlaySFXAdjusted(int sfxToAdjust)
    {
        
        sfx[sfxToAdjust].pitch = UnityEngine.Random.Range(.8f, 102f);
        PlayAFX(sfxToAdjust); ;
    }




}