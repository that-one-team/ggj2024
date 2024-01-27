using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


[System.Serializable]
public class EventTest
{
    public string Name;
    public HumorStats StatsAffected;
    public float ReputationEffect;
    public float EventChance;
    public float CurrentEventChance;
    public float EventChanceAdditional;
}

public class EventManager : MonoBehaviour
{
    [SerializeField] List<EventTest> _events;


    // private EventData[] _events;
    // public EventData EventData;
    public PlayerReputation playerReputation;

    //For Event Proc
    public float cooldownDuration;
    private float nextGenerateTime;
    private System.Random random = new System.Random();
    public bool fakeNews;
    public bool controversial;
    public bool pastIssues;




    // For Phone UI
    public Transform imageToScale;
    public float speed = 2.0f;
    public Vector3 phoneTargetPos;
    public Vector3 phoneDefaultPos;
    public Image screen;

    // For Phone Notification UI
    public GameObject notification1;
    public GameObject notification2;
    public GameObject notification3;
    public GameObject notification4;
    public GameObject notif1;
    public GameObject notif2;
    public GameObject notif3;
    public GameObject fake;
    public GameObject contro;
    public GameObject past;

    public bool currentEvent;

    // For Sound Effects
    public AudioSource audioSource;

    Sprite[] issueSprites;


    // Start is called before the first frame update
    void Start()
    {
        playerReputation = GameObject.Find("Player").GetComponent<PlayerReputation>();
        audioSource = GetComponent<AudioSource>();

        var loaded = Resources.LoadAll("Events/Articles", typeof(Sprite));
        issueSprites = new Sprite[loaded.Length];

        loaded.CopyTo(issueSprites, 0);
        print(issueSprites[0].name);

        currentEvent = false;
    }

    // Update is called once per frame
    void Update()
    {
        EventProc();

    }

    public void RunEvent(EventTest evt)
    {
        playerReputation.Reputation += evt.ReputationEffect;

        var sprite = issueSprites.SelectRandom();
        screen.sprite = sprite;
    }

    public void EventProc()
    {
        if (Time.time >= nextGenerateTime && !currentEvent)
        {
            var randomEvent = _events.SelectRandom(); 
            int generatedNumber = GenerateNumber();
            Debug.Log(generatedNumber);
            nextGenerateTime = Time.time + cooldownDuration;

            

            if (generatedNumber <= randomEvent.CurrentEventChance)
            {
                RunEvent(randomEvent);
                StartCoroutine(Timer());
                for (int i = 0; i < _events.Count; i++)
                {
                    _events[i].CurrentEventChance = _events[i].EventChance;
                }

                if (randomEvent == _events[0])
                {

                }
                else if (randomEvent == _events[1])
                {

                }
                else if (randomEvent == _events[2])
                {

                }

                currentEvent = true;


            }
            else
            {
                randomEvent.CurrentEventChance += randomEvent.EventChanceAdditional;
            }

           
        }
    }

    int GenerateNumber()
    {
        return random.Next(1, 101);
    }

    public IEnumerator Timer()
    {
        PhoneOn();
        yield return new WaitForSeconds(6f);
        //PhoneOff();
        yield return new WaitForSeconds(2f);

        

        //notif1.SetActive(false);
        //notif2.SetActive(false);
        //notif3.SetActive(false);
        //pastIssues = false;
        //past.SetActive(false);
        //fakeNews = false;
        //fake.SetActive(false);
        //controversial = false;
        //contro.SetActive(false);

        

    } 

    public void PhoneOn()
    {
        imageToScale.DOLocalMoveX(250f, 0.5f);
        StartCoroutine(FadeIn());
    }

    //public void PhoneOff()
    //{
    //    imageToScale.transform.DOLocalMoveX(550f, 0.5f);
    //}

    public IEnumerator FadeIn()
    {
        var randomNumber1 = random.Next(1, 101);
        var randomNumber2 = random.Next(1, 101);

        if (randomNumber1 <= 50)
        {
            notif1 = notification1;
        }
        else
        {
            notif1 = notification2;
        }

        if (randomNumber2 <= 50)
        {
            notif2 = notification3;
        }
        else
        {
            notif2 = notification4;
        }

       

        yield return new WaitForSeconds(2f);
        audioSource.Play();
        notif1.SetActive(true);
        yield return new WaitForSeconds(0.25f); 
        audioSource.Play();
        notif2.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        audioSource.Play();
        notif3.SetActive(true);


    }

    public void FakeNews()
    {
        if (fakeNews)
        {
            fake.SetActive(true);
        }
    }

    public void Controversial()
    {
        if (controversial)
        {
            contro.SetActive(true);
        }
    }

    public void PastIssues()
    {
        if (pastIssues)
        {
            past.SetActive(true);
        }
    }

}
