using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //[SerializeField]
    private Vector3 _destination;
    private NavMeshAgent _ai;

    private void OnEnable()
    {
        Debug.Log("Enemy enabled");
        _ai = GetComponent<NavMeshAgent>();
        if (_ai != null)
        {
            _destination = WaypointManager.Instance.GetDestination().position;
            _ai.SetDestination(WaypointManager.Instance.GetDestination().position);
        }
    }

    public void MoveTowardsDestination(Vector3 destination)
    {
        _destination = destination;
        _ai.SetDestination(destination);
        _ai.isStopped = false;
    }

    public void StopMoving()
    {
        if (_ai.isOnNavMesh)
        {
            _ai.isStopped = true;
        }
    }
    public void ResumeMoving()
    {
        _ai.isStopped = false;
    }
    /*
    public float DistanceRemaining()
    {
        return Vector3.SqrMagnitude(transform.position - _destination);
    }*/
}
