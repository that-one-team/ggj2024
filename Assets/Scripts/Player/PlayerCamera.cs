using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed = 10;
    [SerializeField] private Transform _target;

    private Vector3 _offset;
    private void Start()
    {
        _offset = transform.localPosition;
        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        var targPos = _target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targPos, _lerpSpeed * Time.deltaTime);
    }
}
