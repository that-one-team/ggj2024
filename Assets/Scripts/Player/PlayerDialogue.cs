using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerDialogue : MonoBehaviour
{
    public static PlayerDialogue Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }
    [SerializeField] NPCDialogueData _dialogueData;

    [Header("UI")]
    [SerializeField] GameObject _dialoguePanel;
    [SerializeField] TextMeshProUGUI _dialogueText;
    [SerializeField] float _charsPerSecond;

    [Space]
    [SerializeField] GameObject _choicesPanel;
    [SerializeField] GameObject _choicePrefab;

    NPCBehavior _target;

    private string _line;

    public void StartConversation(GameObject target)
    {
        Camera.main.GetComponent<PlayerCamera>().AddToFocus(target.transform);
        _target = target.GetComponent<NPCBehavior>();
        _target.GetComponent<NPCMovement>().ProcessState((int)NPCState.INTERACTING);

        _line = _dialogueData.GetMaxStatLines(_target.Data.Stats).SelectRandom();

        _dialoguePanel.SetActive(true);
        ClearText();

        StartCoroutine(Typing());
    }

    public void ClearText()
    {
        _dialogueText.text = "";
    }

    IEnumerator Typing()
    {
        foreach (char letter in _line.ToCharArray())
        {
            _dialogueText.text += letter;
            yield return new WaitForSeconds(_charsPerSecond / 60);
        }
    }

    void Update()
    {
        if (_dialogueText.text == _line)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Next();
            }
        }
    }

    public void Next()
    {
        if (_dialogueText.text == _line)
        {
            ClearText();
            ShowChoices();
        }
    }

    // prepare for animation/cutscene use
    public void ShowChoices()
    {
        _dialoguePanel.SetActive(false);
        _choicesPanel.SetActive(true);

        LoadChoices();
    }

    void LoadChoices()
    {
        foreach (Transform child in _choicesPanel.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in PlayerInventory.Instance.Items)
        {
            var spawned = Instantiate(_choicePrefab, _choicesPanel.transform).GetComponent<UIInventoryItem>();
            spawned.Data = item;
            spawned.IsInteractable = true;
            spawned.Target = _target;
            spawned.OnInteract += () =>
            {
                StartCoroutine(EndConversation());
            };

            spawned.UpdateVisuals();
        }
    }

    public IEnumerator EndConversation()
    {
        Camera.main.GetComponent<PlayerCamera>().RemoveFromFocus(_target.transform);
        ClearText();
        _dialoguePanel.SetActive(false);
        _choicesPanel.SetActive(false);
        yield return new WaitForSeconds(1.1f);
        _target = null;
        PlayerInteraction.Instance.Interact(false, null);
    }
}