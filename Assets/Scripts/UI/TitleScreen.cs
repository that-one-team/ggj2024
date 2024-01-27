using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    public GameObject TitleInterface, StoryText;
    public GameObject[] Pugsly;
    public Animation StoryAnimator;
    public AnimationClip StorySequence;

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
            HideStart();
            StartStorySequence();
        }
    }
    private void StartStorySequence()
    {
        StoryAnimator.gameObject.SetActive(true);
        StoryAnimator.clip = StorySequence;
        StoryAnimator.Play();
    }
    private void HideStart()
    {
        foreach(var item in Pugsly)
        {
            item.gameObject.SetActive(false);
        }
    }
}
