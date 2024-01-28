using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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
    [SerializeField] TextMeshProUGUI _timeLeft;

    [Header("Notification settings")]
    [SerializeField] List<string> _notifications;
    [SerializeField] Transform _notificationsContainer;
    [SerializeField] GameObject _notificationPrefab;
    [SerializeField] AudioClip _notificationSound;
    [SerializeField] AudioClip _vibrationSound;

    public bool IsEventActive { get; private set; }
    public EventData CurrentEvent { get; private set; }

    AudioSource _audio;
    Sprite[] _controversialIssues;
    Sprite[] _tweetIssues;

    float _nextGenerateTime;
    bool _hasResponded;

    [SerializeField] float _replyTime = 11;
    float _replyTimeLeft = 7;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _replyTimeLeft = _replyTime;

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

        if (!_hasResponded && IsEventActive)
        {
            _replyTimeLeft -= Time.deltaTime;

            if (_replyTimeLeft <= 0)
            {
                StartCoroutine(CompleteEvent(false));
            }
        }

        _timeLeft.text = ((int)_replyTimeLeft).ToString();
    }

    void EventProc()
    {
        if (Time.time >= _nextGenerateTime && !IsEventActive)
        {
            CurrentEvent = _events.SelectRandom();
            int roll = Random.Range(0, 100);

            _nextGenerateTime = Time.time + _eventCooldownDuration;

            if (roll <= CurrentEvent.CurrentEventChance)
            {
                RunEvent(CurrentEvent);
            }
            else
            {
                CurrentEvent.CurrentEventChance += CurrentEvent.EventChanceAdditional;
            }
        }
    }

    void RunEvent(EventData eventData)
    {
        _replyTimeLeft = _replyTime;
        IsEventActive = true;

        var issues = eventData.Name.ToLower().Equals("fake news") ? _tweetIssues : _controversialIssues;

        var sprite = issues.SelectRandom();
        _phoneScreen.sprite = sprite;

        StartCoroutine(PlayNotifications());
    }

    IEnumerator PlayNotifications()
    {
        _audio.Stop();
        _phone.DOLocalMoveX(600f, 0.5f);

        yield return new WaitForSeconds(1);
        for (int i = 0; i < 3; i++)
        {
            var notif = _notifications.SelectRandom();

            var notifUI = Instantiate(_notificationPrefab, _notificationsContainer).GetComponent<UINotification>();
            notifUI.ShowNotif(notif);

            _audio.PlayOneShot(_notificationSound);
            _audio.PlayOneShot(_vibrationSound);
            yield return new WaitForSeconds(0.25f);
        }

        yield return null;
    }

    public void RespondToEvent()
    {
        if (_hasResponded) return;
        _hasResponded = true;
        var response = _responses.SelectRandom();
        var notifUI = Instantiate(_notificationPrefab, _notificationsContainer).GetComponent<UINotification>();
        notifUI.ShowNotif(response, true);

        StartCoroutine(CompleteEvent(true));
    }

    public IEnumerator CompleteEvent(bool isSuccess, bool force = false)
    {
        _hasResponded = true;

        if (!force)
        {
            yield return new WaitForSeconds(2);
        }

        _phone.DOLocalMoveX(1400, 0.1f);
        _hasResponded = false;
        IsEventActive = false;
        _replyTimeLeft = _replyTime;

        foreach (Transform notif in _notificationsContainer)
        {
            Destroy(notif.gameObject);
        }

        if (!isSuccess)
            PlayerReputation.Instance.AddRep(CurrentEvent.ReputationEffect);
        CurrentEvent = null;
    }
}
