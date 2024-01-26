using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NPCMovement : MonoBehaviour
{
    [field: SerializeField] public int CurrentState { get; private set; } = (int)NPCState.IDLE;
    private NavMeshAgent _agent;
    private Coroutine _currRoutine;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        ProcessState(0);
    }

    void ProcessState(int state)
    {
        CurrentState = state;
        if (state == 0)
            ChangeState(IdleState());
        else if (state == 1)
            ChangeState(MovingState());
        else if (state == 2)
            ChangeState(InteractingState());
        else
            ProcessState(0);

    }

    IEnumerator IdleState()
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(RandomTime());
        RandomState();
    }

    IEnumerator MovingState()
    {
        yield return new WaitForSeconds(RandomTime());
    }

    IEnumerator InteractingState()
    {
        yield return new WaitUntil(() => !PlayerInteraction.Instance.IsInteracting);
    }

    float RandomTime() => Random.Range(3, 10);

    void RandomState()
    {
        int roll = Random.Range(0, 3);
        ProcessState(roll);
    }

    void ChangeState(IEnumerator coroutine)
    {
        if (_currRoutine != null)
        {
            StopCoroutine(_currRoutine);
            _currRoutine = null;
        }

        _currRoutine = StartCoroutine(coroutine);
    }
}