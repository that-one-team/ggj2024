using System.Collections;
using Unity.VisualScripting;
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

        _agent.baseOffset = 5.85f;

        RandomState();
    }

    public void ProcessState(int state)
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

        print($"[AI]: {name}: Change state -> {state}");
    }

    IEnumerator IdleState()
    {
        _agent.isStopped = true;
        yield return new WaitForSeconds(RandomTime());
        RandomState();
    }

    IEnumerator MovingState()
    {
        _agent.isStopped = false;
        _agent.SetDestination(GetRandomDestination());
        yield return new WaitUntil(() => _agent.remainingDistance <= 0.01f);
        _agent.isStopped = true;
        RandomState();
    }

    IEnumerator InteractingState()
    {
        yield return new WaitUntil(() => !PlayerInteraction.Instance.IsInteracting);
    }

    float RandomTime() => Random.Range(3, 10);

    void RandomState()
    {
        int roll = Random.Range(0, 2);
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

    Vector3 GetRandomDestination()
    {
        var tris = NavMesh.CalculateTriangulation();
        Mesh mesh = new()
        {
            vertices = tris.vertices,
            triangles = tris.indices
        };

        return GetRandomPointOnMesh(mesh);
    }

    // not mine! https://gist.github.com/v21/5378391#file-gistfile1-cs-L38
    Vector3 GetRandomPointOnMesh(Mesh mesh)
    {
        float[] sizes = GetTriSizes(mesh.triangles, mesh.vertices);
        float[] cumulativeSizes = new float[sizes.Length];
        float total = 0;

        for (int i = 0; i < sizes.Length; i++)
        {
            total += sizes[i];
            cumulativeSizes[i] = total;
        }

        float randomsample = Random.value * total;

        int triIndex = -1;

        for (int i = 0; i < sizes.Length; i++)
        {
            if (randomsample <= cumulativeSizes[i])
            {
                triIndex = i;
                break;
            }
        }

        if (triIndex == -1) Debug.LogError("triIndex should never be -1");

        Vector3 a = mesh.vertices[mesh.triangles[triIndex * 3]];
        Vector3 b = mesh.vertices[mesh.triangles[triIndex * 3 + 1]];
        Vector3 c = mesh.vertices[mesh.triangles[triIndex * 3 + 2]];

        //generate random barycentric coordinates

        float r = Random.value;
        float s = Random.value;

        if (r + s >= 1)
        {
            r = 1 - r;
            s = 1 - s;
        }
        //and then turn them back to a Vector3
        Vector3 pointOnMesh = a + r * (b - a) + s * (c - a);
        return pointOnMesh;

    }

    float[] GetTriSizes(int[] tris, Vector3[] verts)
    {
        int triCount = tris.Length / 3;
        float[] sizes = new float[triCount];
        for (int i = 0; i < triCount; i++)
        {
            sizes[i] = .5f * Vector3.Cross(verts[tris[i * 3 + 1]] - verts[tris[i * 3]], verts[tris[i * 3 + 2]] - verts[tris[i * 3]]).magnitude;
        }
        return sizes;

        /*
         * 
         * more readably:
         * 
for(int ii = 0 ; ii < indices.Length; ii+=3)
{
    Vector3 A = Points[indices[ii]];
    Vector3 B = Points[indices[ii+1]];
    Vector3 C = Points[indices[ii+2]];
    Vector3 V = Vector3.Cross(A-B, A-C);
    Area += V.magnitude * 0.5f;
}
         * 
         * 
         * */
    }
}