using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public TitleButtons TitleButtons;

    public void CallNextScene()
    {
        TitleButtons.StartGame();
    }
}
