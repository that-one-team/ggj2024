using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public GameObject TitleInterface, StoryText;

    public void StoryButton()
    {
        TitleInterface.SetActive(false);
        StoryText.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

}
