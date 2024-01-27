using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINPCStats : MonoBehaviour
{
    public static UINPCStats Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public HumorStats TargetData { get; set; }

    [field: SerializeField]
    public GameObject StatsGroup { get; private set; }
    [SerializeField] GameObject _statsPanel;
    [SerializeField] Slider[] _humorSlider;

    void Start()
    {
        StatsGroup.SetActive(false);
        _statsPanel.SetActive(false);
    }

    public void SetTarget(HumorStats target)
    {
        _statsPanel.SetActive(false);
        TargetData = target;
        UpdateVisuals();
    }

    public void ToggleStats()
    {
        _statsPanel.SetActive(!_statsPanel.activeSelf);
        UpdateVisuals();
    }

    public void UpdateVisuals()
    {
        for (int i = 0; i < _humorSlider.Length; i++)
        {
            var slider = _humorSlider[i];
            var data = TargetData.GetStatsInOrder()[i];
            slider.maxValue = 10;
            slider.value = data;
        }
    }
}
