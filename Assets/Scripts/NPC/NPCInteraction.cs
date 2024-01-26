using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    public GameObject DialougePanel;
    public Text DialougeText;
    private int index;
    public GameObject contbutton;
    public float wordSpeed;
    public bool playerIsClose;

    public GameObject indicator;

    private string[] _lines;

    private NPCBehavior _behaviour;

    private void Awake()
    {
        _lines = Resources.Load<NPCDialogueData>("DialogueLines").GetMaxStatLines(_behaviour.Data.Stats);
    }

    private void Start()
    {
        _behaviour = GetComponent<NPCBehavior>();
        indicator = transform.GetChild(0).gameObject;

    }

    void Update()
    {
        if (_lines.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.E) && playerIsClose)
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

            PlayerInteraction.Instance.Interact(true);
        }

        if (DialougeText.text == _lines[index])
        {
            contbutton.SetActive(true);
        }
    }

    public void zeroText()
    {
        DialougeText.text = "";
        index = 0;
        DialougePanel.SetActive(false);
        PlayerInteraction.Instance.Interact(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in _lines[index].ToCharArray())
        {
            DialougeText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contbutton.SetActive(false);

        if (index < _lines.Length - 1)
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
