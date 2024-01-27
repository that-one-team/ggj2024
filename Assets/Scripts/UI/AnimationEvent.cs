using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    // public TitleButtons TitleButtons;
    public LoadingScreen LoadingScreen;

    public void CallNextScene()
    {
        LoadingScreen.LoadScreen(1);

        // TitleButtons.StartGame();
    }
}
