using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UINotification : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _author;

    public void ShowNotif(string content, bool isPugslySender = false)
    {
        _text.text = content;

        _author.text = isPugslySender ? "Pugsly Pandesal" : $"user" + Random.Range(1000, 9999);
    }

    public void CloseNotif()
    {
        Destroy(gameObject);
    }
}
