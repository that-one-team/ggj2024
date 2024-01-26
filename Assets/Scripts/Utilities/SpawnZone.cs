using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }

    public Vector3 GetRandomPosition()
    {
        var size = transform.localScale;
        return new Vector3(transform.position.x + Random.Range(-size.x, size.x), 1, transform.position.z + Random.Range(-size.z, size.z));
    }
}