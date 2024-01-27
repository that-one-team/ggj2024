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

    private string _line;

    public void StartConversation(GameObject target)
    {
        Camera.main.GetComponent<PlayerCamera>().ToggleFocusCamera(PlayerInteraction.Instance.transform, transform);
        var _behaviour = target.GetComponent<NPCBehavior>();
        _behaviour.GetComponent<NPCMovement>().ProcessState((int)NPCState.INTERACTING);

        _line = _dialogueData.GetMaxStatLines(_behaviour.Data.Stats).SelectRandom();

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
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space))
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
            spawned.OnInteract += () =>
            {
                print("USE ITEM");
                EndConversation();
            };
        }
    }

    public void EndConversation()
    {
        Camera.main.GetComponent<PlayerCamera>().ToggleFocusCamera();
        ClearText();
        _dialoguePanel.SetActive(false);
        _choicesPanel.SetActive(false);
        PlayerInteraction.Instance.Interact(false, null);
    }
}