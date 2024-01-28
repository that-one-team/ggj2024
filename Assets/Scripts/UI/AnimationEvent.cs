using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // public TitleButtons TitleButtons;
    public LoadingScreen LoadingScreen;
    public AudioSource AudioSource;
    public AudioClip BrightMusic;
    public AudioClip DarkMusic;
    public void CallNextScene()
    {
        LoadingScreen.LoadScreen(1);
    }
    public void PlayBrightMusic()
    {
        AudioSource.PlayOneShot(BrightMusic);
    }
    public void EndMusic()
    {
        AudioSource.Stop();
    }
    public void PlayEndMusic()
    {
        AudioSource.PlayOneShot(DarkMusic);
        //if (AudioSource.isPlaying) { AudioSource.Stop(); }
        //AudioSource.PlayOneShot(DarkMusic);
    }
}
