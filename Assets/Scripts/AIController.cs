using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Transform endPoint;
    public int Score { get; set; }
    [SerializeField]
    private State currentState;
    private EnemyMovement enemyMovement;
    private float distanceThreshold = 5f;

    private Animator _anim;

    private CapsuleCollider _collider;

    private bool _goingToHidingSpot = false;

    private void Start()
    {
        _collider = gameObject.GetComponent<CapsuleCollider>();
        if (_collider == null)
        {
            Debug.Log("Enemy Collider is NULL");
        }
        _anim = GetComponent<Animator>();
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


    public void HideAnimationStart()
    {
        _anim.SetBool("Hiding",true);

    }
    public void HideAnimationStop()
    {
        _anim.SetBool("Hiding", false);

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

    public void UpdateDestination()
    {
        enemyMovement.MoveTowardsDestination(endPoint.position);
    }
    public void UpdateDestination(Vector3 position)
    {

        if(position == null || WillTurnAround(position))
        {
            UpdateDestination();
        }
        else
        {
            enemyMovement.MoveTowardsDestination(position);
            _goingToHidingSpot = true;

        }
    }

    public bool WillTurnAround(Vector3 destination)
    {
        //if enemy will run back up stairs to a barrier, dont
        if (destination.y-1 > transform.position.y)
            return true;

        // Get the direction from the agent's position to the destination
        Vector3 directionToDestination = destination - transform.position;

        // Calculate the angle between the agent's forward direction and the direction to the destination
        float angle = Vector3.Angle(transform.forward, directionToDestination);

        // Determine if the angle is greater than 90 degrees (turning backwards)
        return angle > 90f;

    }

    public void HideComplete()
    {
        _goingToHidingSpot = false;
    }

    public bool ShouldHide()
    {
        return _goingToHidingSpot;
    }
    /*
    public float RemainingDistance()
    {
        Debug.Log("Distance remaining: " + enemyMovement.DistanceRemaining());
        return enemyMovement.DistanceRemaining();
    }*/

    public void StopMoving()
    {
        enemyMovement.StopMoving();
    }
    public void ResumeMoving()
    {
        enemyMovement.ResumeMoving();
    }

    public void DeathAnimationStart()
    {
        _anim.SetBool("Dead", true);
        StartCoroutine(SetInactiveRoutine());
    }
    public void DeathAnimationStop()
    {
        _anim.SetBool("Dead", false);
    }

    public void TellEnemiesToHide()
    {
        foreach (GameObject obj in ObjectPoolManager.Instance.GetEnemyList())
        {
            float distance = Vector3.Distance(obj.transform.position, transform.position);
            if (distance <= distanceThreshold)
            {
                AIController aiController = obj.GetComponent<AIController>();
                if (aiController != null)
                {
                    aiController.ChangeState(new HideState(aiController));
                }
            }
        }
    }
    public void ShrinkCollider()
    {
        _collider.height = 1f;
        _collider.center = new Vector3(0,0.5f,0);
    }
    public void ExpandCollider()
    {
        _collider.height = 1.5f;
        _collider.center = new Vector3(0, 0.75f, 0);

    }
    public void DisableCollider()
    {
        _collider.enabled = false;
    }
    public void EnableCollider()
    {
        _collider.enabled = true;
    }

    IEnumerator SetInactiveRoutine()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}