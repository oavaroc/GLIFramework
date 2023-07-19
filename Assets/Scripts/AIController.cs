using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Transform endPoint;
    public int Score { get; set; }

    private State currentState;
    private EnemyMovement enemyMovement;

    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        if (enemyMovement == null)
        {
            Debug.Log("Enemy Movement is NULL");
        }

        endPoint = WaypointManager.Instance.GetDestination();
        // Start with the Run state
        currentState = new RunState(this);
        currentState.Enter();
    }

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(State newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void MoveTowardsEndPoint()
    {
        enemyMovement.MoveTowardsDestination(endPoint.position);
    }

    public bool ShouldHide()
    {
        // To be implemented, determine logic for when ai should hide later, for now return false to always run
        return false;
    }

    public void StopMoving()
    {
        enemyMovement.StopMoving();
    }

    public void DeathAnimation()
    {
        gameObject.SetActive(false);
    }
}