using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{
    public GameObject TitleInterface, StoryText;

    //Button For Start of the Story
    public void StoryButton()
    {
        TitleInterface.SetActive(false);
        StoryText.SetActive(true);
    }
    //Start of The Dialogue 
    public void Start_Of_The_Game()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
