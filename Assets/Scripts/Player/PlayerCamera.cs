using System.Linq;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _lerpSpeed = 10;
    [SerializeField] private Transform _target;
    private Vector3 _targetPosition;
    private Vector3 _offset;
    private Vector3 _lastPos;

    bool _isFocusCam;

    private void Start()
    {
        _offset = transform.localPosition;
        if (_target == null) _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (!_isFocusCam)
            _targetPosition = _target.position + _offset;

        transform.position = Vector3.Lerp(transform.position, _targetPosition, _lerpSpeed * Time.deltaTime);
    }

    public void ToggleFocusCamera(params Transform[] targets)
    {
        // _isFocusCam = !_isFocusCam;
        // _lastPos = transform.position;

        // if (targets.Length == 1)
        // {
        //     _targetPosition = targets[0].position + _offset;
        //     return;
        // }

        // var bounds = new Bounds(targets[0].position, Vector3.zero);
        // foreach (var target in targets)
        // {
        //     bounds.Encapsulate(target.position);
        // }

        // _targetPosition = bounds.center + _offset;
    }
}
