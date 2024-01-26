using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    public TextMeshProUGUI reputation;
    public Slider socialBattery;
    public float maxSocialBattery = 100f;
    public float currentSocialBattery;

    public PlayerReputation playerReputation;
    public PlayerSocialBattery playerSocialBattery;


    // Start is called before the first frame update
    void Start()
    {
        playerReputation = GameObject.Find("Player").GetComponent<PlayerReputation>();
        playerSocialBattery = GameObject.Find("Player").GetComponent<PlayerSocialBattery>();

        currentSocialBattery = playerSocialBattery.socialBattery;
        socialBattery.maxValue = maxSocialBattery;
        socialBattery.value = currentSocialBattery;
    }

    // Update is called once per frame
    void Update()
    {
        currentSocialBattery = playerSocialBattery.socialBattery;
        reputation.text = "Reputation: " + playerReputation.reputation;

        socialBattery.value = currentSocialBattery;

       
    }
}
