using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
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
            UpdateDestination();
            StartCoroutine(MoveToNextWaypoint());
        }

    }

    private void UpdateDestination()
    {
        transform.position = WaypointManager.Instance.GetSpawnPoint().position;
        _ai.SetDestination(_destination);
    }

    private IEnumerator MoveToNextWaypoint()
    {
        NavMeshPath path = new NavMeshPath();

        _ai.CalculatePath(_destination, path);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            while (true)
            {
                if (_ai.hasPath)
                {
                    float distanceToDestination = _ai.remainingDistance;
                    Debug.Log("Actual Distance to Destination: " + distanceToDestination);

                    if (distanceToDestination < 1f)
                    {
                        Debug.Log("Reached Destination, Deactivating");
                        gameObject.SetActive(false);
                    }
                }

                yield return new WaitForSeconds(0.2f); // Wait for the next frame
            }
        }
        else
        {
            Debug.LogWarning("No valid path to the destination!");
            gameObject.SetActive(false);
        }
    }
}
