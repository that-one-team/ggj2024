using System.Linq;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup _targetGroup;
    private CinemachineVirtualCamera _vcam;
    private float _startFOV;

    [SerializeField] private float _talkFOV;

    private void Start()
    {
        _vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        _startFOV = _vcam.m_Lens.FieldOfView;
    }

    public void AddToFocus(Transform target)
    {
        _targetGroup.AddMember(target, 1, 1);

        float fov = _vcam.m_Lens.FieldOfView;
        DOTween.To(() => fov, x => fov = x, _talkFOV, 0.5f).OnUpdate(() =>
        {
            _vcam.m_Lens.FieldOfView = fov;
        });
    }

    public void RemoveFromFocus(Transform target)
    {
        _targetGroup.RemoveMember(target);

        float fov = _vcam.m_Lens.FieldOfView;
        DOTween.To(() => fov, x => fov = x, _startFOV, 0.5f).OnUpdate(() =>
        {
            _vcam.m_Lens.FieldOfView = fov;
        });
    }
}
