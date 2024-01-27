using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Mood
{
    public float Threshold;
    public Sprite Emote;
}

public class NPCMood : MonoBehaviour
{
    [field: SerializeField]
    public float CurrentMood { get; private set; }

    [SerializeField] private GameObject _moodBubble;
    [SerializeField] private Image _emoteIcon;
    [SerializeField] private Mood[] _moods;
    [SerializeField] private Sprite _confusedEmote;

    private void Start()
    {
        SetMood(Random.Range(20, 40));
    }

    public void AddMood(float val)
    {
        print(val);
        bool showConfused = val < 0;
        SetMood(CurrentMood + val, showConfused ? _confusedEmote : _moods[0].Emote);
    }

    void SetMood(float value, Sprite emote = null)
    {
        CurrentMood = value;
        if (emote != null)
        {
            StartCoroutine(ShowEmote(emote));
            return;
        }

        foreach (var mood in _moods)
        {
            if (CurrentMood <= mood.Threshold)
            {
                _emoteIcon.sprite = mood.Emote;
            }
        }

        StartCoroutine(ShowBubble());
    }

    IEnumerator ShowBubble()
    {
        while (true)
        {
            _moodBubble.SetActive(false);
            yield return new WaitForSeconds(Random.Range(10, 20));
            yield return ShowEmote(_emoteIcon.sprite);
        }
    }

    IEnumerator ShowEmote(Sprite emote)
    {
        _emoteIcon.sprite = emote;
        _moodBubble.SetActive(true);
        yield return new WaitForSeconds(5);
        _moodBubble.SetActive(false);
    }
}