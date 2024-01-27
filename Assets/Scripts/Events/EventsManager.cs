using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class EventsManager : MonoBehaviour
{
    [Header("Event settings")]
    [SerializeField] List<EventData> _events;
    [SerializeField] float _eventCooldownDuration;
    [SerializeField] List<string> _responses;

    [Header("UI Settings")]
    [SerializeField] Transform _phone;
    [SerializeField] Image _phoneScreen;

    [Header("Notification settings")]
    [SerializeField] List<string> _notifications;
    [SerializeField] Transform _notificationsContainer;
    [SerializeField] GameObject _notificationPrefab;

    public bool IsEventActive { get; private set; }

    AudioSource _audio;
    Sprite[] _controversialIssues;
    Sprite[] _tweetIssues;

    float _nextGenerateTime;

    private void Start()
    {

        _audio = GetComponent<AudioSource>();

        var loadedC = Resources.LoadAll("Events/Articles/Controversials", typeof(Sprite));
        _controversialIssues = new Sprite[loadedC.Length];

        loadedC.CopyTo(_controversialIssues, 0);

        var loadedT = Resources.LoadAll("Events/Articles/Controversials", typeof(Sprite));
        _tweetIssues = new Sprite[loadedT.Length];

        loadedT.CopyTo(_tweetIssues, 0);
    }

    void Update()
    {
        EventProc();
    }

    void EventProc()
    {
        if (Time.time >= _nextGenerateTime && !IsEventActive)
        {
            var evt = _events.SelectRandom();
            int roll = Random.Range(0, 100);

            _nextGenerateTime = Time.time + _eventCooldownDuration;

            if (roll <= evt.CurrentEventChance)
            {
                RunEvent(evt);
            }
            else
            {
                evt.CurrentEventChance += evt.EventChanceAdditional;
            }
        }
    }

    void RunEvent(EventData eventData)
    {
        IsEventActive = true;
        PlayerReputation.Instance.Reputation += eventData.ReputationEffect;

        var issues = eventData.Name.ToLower().Equals("fake news") ? _tweetIssues : _controversialIssues;

        var sprite = issues.SelectRandom();
        _phoneScreen.sprite = sprite;

        StartCoroutine(PlayNotifications());
    }

    IEnumerator PlayNotifications()
    {
        _audio.Stop();
        _phone.DOLocalMoveX(250f, 0.5f);
        string lastNotif = "";

        for (int i = 0; i < Random.Range(0, 3); i++)
        {
            var notif = _notifications.SelectRandom();
            if (notif.Equals(lastNotif)) continue; // if same notification from before, skip
            lastNotif = notif;

            var notifUI = Instantiate(_notificationPrefab, _notificationsContainer).GetComponent<UINotification>();
            notifUI.ShowNotif(notif);

            _audio.Play();
            yield return new WaitForSeconds(0.25f);
        }

        yield return null;
    }

    public void RespondToEvent()
    {
        var response = _responses.SelectRandom();
        var notifUI = Instantiate(_notificationPrefab, _notificationsContainer).GetComponent<UINotification>();
        notifUI.ShowNotif(response, true);

        StartCoroutine(CompleteEvent());
    }

    public IEnumerator CompleteEvent()
    {
        yield return new WaitForSeconds(2);
        _phone.DOLocalMoveX(600, 0.5f);
        IsEventActive = false;
    }
}
