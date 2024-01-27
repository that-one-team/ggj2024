using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] Text _dialogueText;
    [SerializeField] GameObject _continueButton;
    [SerializeField] float _wordSpeed;
    [SerializeField] GameObject _indicatorUI;

    private int _lineIndex;
    private bool _isPlayerClose;
    private string[] _lines;

    private NPCBehavior _behaviour;

    private void Start()
    {
        _behaviour = GetComponent<NPCBehavior>();
        IndicatorOff();
    }

    void Update()
    {
        if (_lines == null || _lines.Length == 0) return;

        if (Input.GetKeyDown(KeyCode.E) && _isPlayerClose && !PlayerInteraction.Instance.IsInteracting)
        {
            if (_dialoguePanel.activeInHierarchy)
            {
                ClearText();
            }
            else
            {
                _dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }

            PlayerInteraction.Instance.Interact(true, gameObject);
        }

        if (_dialogueText.text == _lines[_lineIndex])
        {
            _continueButton.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
            {
                NextLine();
            }
        }
    }

    public void ClearText()
    {
        // _dialogueText.text = "";
        _lineIndex = 0;
        // _dialoguePanel.SetActive(false);
        // PlayerInteraction.Instance.Interact(false);
    }

    IEnumerator Typing()
    {
        foreach (char letter in _lines[_lineIndex].ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_wordSpeed);
        }
    }

    public void NextLine()
    {
        _continueButton.SetActive(false);

        if (_lineIndex < _lines.Length - 1)
        {
            _lineIndex++;
            _dialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            ClearText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerClose = true;
            IndicatorOn();
            ClearText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerClose = false;
            IndicatorOff();
            ClearText();
        }
    }

    private void IndicatorOn()
    {
        _indicatorUI.SetActive(true);
    }

    private void IndicatorOff()
    {
        _indicatorUI.SetActive(false);
    }
}
