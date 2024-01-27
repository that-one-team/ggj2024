using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float Amplitude = 10, Frequency = 20;

    Vector3 _offset;
    void Start()
    {
        _offset = transform.position;
    }

    void Update()
    {
        var x = Mathf.Sin(Time.time * Amplitude) * Frequency;
        var z = Mathf.Cos(Time.time * Amplitude) * Frequency;
        transform.position = new Vector3(x, z, 0) + _offset;
    }
}
