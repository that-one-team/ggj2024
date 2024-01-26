using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    public GameObject DialougePanel;
    public Text DialougeText;
    public string[] Dialouge;
    private int index;
    public GameObject contbutton;
    public float wordSpeed;
    public bool playerIsClose;

    public GameObject indicator;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)&& playerIsClose)
        {
            if (DialougePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                DialougePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (DialougeText.text == Dialouge[index])
        {
            contbutton.SetActive(true);
        }
    }

    public void zeroText()
    {
        DialougeText.text = "";
        index = 0;
        DialougePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in Dialouge[index].ToCharArray())
        {
            DialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contbutton.SetActive(false);

        if (index < Dialouge.Length - 1)
        {
            index++;
            DialougeText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            IndicatorOn();
            zeroText();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            IndicatorOff();
            zeroText();
        }
    }

    private void IndicatorOn()
    {
        indicator.SetActive(true);
    }

    private void IndicatorOff()
    {
        indicator.SetActive(false);
    }
}
