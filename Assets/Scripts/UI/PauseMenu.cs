using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public void Resume()
    {
        gameObject.SetActive(false);
    }

    public void BackToMenu()
    {
        GameManager.Instance.BackToMenu();
    }
}
