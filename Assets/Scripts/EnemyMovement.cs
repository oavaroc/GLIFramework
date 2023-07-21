using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Vector3 _destination;
    private NavMeshAgent _ai;

    private void OnEnable()
    {
        Debug.Log("Enemy enabled");
        _ai = GetComponent<NavMeshAgent>();
        if (_ai != null)
        {
            _destination = WaypointManager.Instance.GetDestination().position;
            _ai.SetDestination(_destination);
            _ai.speed = Random.Range(5f, 7f);
        }
    }

    public void MoveTowardsDestination(Vector3 destination)
    {
        if (_ai.isOnNavMesh)
        {
            Debug.Log("Moving to destination: " + destination);
            _destination = destination;
            _ai.SetDestination(destination);
            _ai.isStopped = false;
        }
    }


    public void StopMoving()
    {
        if (_ai.isOnNavMesh)
        {
            Debug.Log("Stop moving");
            _ai.isStopped = true;
            _ai.SetDestination(transform.position);
        }
    }
    public void ResumeMoving()
    {
        if (_ai.isOnNavMesh)
        {
            Debug.Log("Resume moving");
            _ai.isStopped = false;
            _ai.speed += 0.1f;
            _ai.SetDestination(WaypointManager.Instance.GetDestination().position);
        }
    }
}
