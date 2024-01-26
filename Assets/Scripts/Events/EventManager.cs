using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

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
    private bool eventActive;

    public GameObject phone;

    // Start is called before the first frame update
    void Start()
    {
        playerReputation = GameObject.Find("Player").GetComponent<PlayerReputation>();

        // _events = Resources.LoadAll<EventData>("Events");
    }

    // Update is called once per frame
    void Update()
    {
        EventProc();

    }

    public void RunEvent(EventTest evt)
    {
        playerReputation.reputation += evt.ReputationEffect;
    }

    public void EventProc()
    {
        if (Time.time >= nextGenerateTime)
        {
            var randomEvent = _events.SelectRandom();
            int generatedNumber = GenerateNumber();
            Debug.Log(generatedNumber);
            nextGenerateTime = Time.time + cooldownDuration;

            if (generatedNumber <= randomEvent.CurrentEventChance)
            {
                RunEvent(randomEvent);
                StartCoroutine(Timer());
                randomEvent.CurrentEventChance = randomEvent.EventChance;
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
        phone.SetActive(true);
        yield return new WaitForSeconds(6f);
        phone.SetActive(false);
    }

}
