using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public EventData EventData;
    public PlayerReputation playerReputation;

    //For Event Proc
    public float cooldownDuration;
    private float nextGenerateTime;
    private System.Random random = new System.Random();
    private float eventChance;
    private bool eventActive;

    public GameObject phone;

    
    

    // Start is called before the first frame update
    void Start()
    {
        playerReputation = GameObject.Find("Player").GetComponent<PlayerReputation>();
        eventChance = 5;
    }

    // Update is called once per frame
    void Update()
    {
        EventProc();

    }

    public void RunEvent()
    {
        playerReputation.reputation += EventData.ReputationEffect;
    }

    public void EventProc()
    {
        

        if (Time.time >= nextGenerateTime)
        {
            int generatedNumber = GenerateNumber();
            Debug.Log(generatedNumber);
            nextGenerateTime = Time.time + cooldownDuration;

            if (generatedNumber <= eventChance)
            {
                FakeNews();
                StartCoroutine(Timer());
                eventChance = 5;
            }
            else
            {
                eventChance += 5;
            }
                
            
        }
    }

    int GenerateNumber()
    {
        return random.Next(1, 101);
    }

    public void FakeNews()
    {
        phone.SetActive(true);
        RunEvent();

    }

    public IEnumerator Timer()
    {
        yield return new WaitForSeconds(6f);
        phone.SetActive(false);
    }
   
}
