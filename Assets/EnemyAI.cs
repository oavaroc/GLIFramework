using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private Transform _destination;

    private NavMeshAgent _ai;

    private void Start()
    {
        _ai = GetComponent<NavMeshAgent>();
        if(_ai != null)
        {
            _ai.SetDestination(_destination.position);
        }
    }

}
