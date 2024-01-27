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

    private void Start()
    {
        SetMood(Random.Range(20, 40));
    }

    public void AddMood(float val)
    {
        SetMood(CurrentMood + val);
    }

    void SetMood(float value)
    {
        CurrentMood = value;

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
            _moodBubble.SetActive(true);
            yield return new WaitForSeconds(5);
        }
    }
}