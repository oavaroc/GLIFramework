using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private Transform _endPoint;
    [SerializeField]
    private State _currentState;
    private EnemyMovement _enemyMovement;
    private float _distanceThreshold = 5f;

    private Animator _anim;

    private CapsuleCollider _collider;

    private bool _goingToHidingSpot = false;

    private bool _isDead = false;

    private bool _neverFear = false;

    [SerializeField]
    private AudioSource _deathSound;

    private void Start()
    {
        if(Random.Range(0f, 100f) < 10f)
        {
            // 10% of the time, the enemy will never hide and run straight for the end
            _neverFear = true;
        }
        _collider = gameObject.GetComponent<CapsuleCollider>();
        if (_collider == null)
        {
            Debug.Log("Enemy Collider is NULL");
        }
        _anim = GetComponent<Animator>();
        _enemyMovement = GetComponent<EnemyMovement>();
        if (_enemyMovement == null)
        {
            Debug.Log("Enemy Movement is NULL");
        }

        _endPoint = WaypointManager.Instance.GetDestination();
        // Start with the Run state
        _currentState = new RunState(this);
        _currentState.Enter();
    }

    public bool GetNeverFear()
    {
        return _neverFear;
    }
    public void SetNeverFear(bool value)
    {
        _neverFear = value;
    }

    public bool GetIsDead()
    {
        return _isDead;
    }

    public void SetIsDead(bool isDead)
    {
        _isDead = isDead;
    }

    public void HideAnimationStart()
    {
        Debug.Log("Hide anim started");
        _anim.SetBool("Hiding",true);

    }
    public void HideAnimationStop()
    {
        Debug.Log("Hide anim stopped");
        _anim.SetBool("Hiding", false);

    }

    private void Update()
    {
        _currentState.Update();
    }

    public void ChangeState(State newState)
    {
        _currentState.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    public void UpdateDestination()
    {
        _enemyMovement.MoveTowardsDestination(_endPoint.position);
    }

    public void HideComplete()
    {
        _goingToHidingSpot = false;
    }

    public bool ShouldHide()
    {
        return _goingToHidingSpot;
    }

    public void StopMoving()
    {
        _enemyMovement.StopMoving();
    }
    public void ResumeMoving()
    {
        _enemyMovement.ResumeMoving();
    }

    public void DeathAnimationStart()
    {
        AudioManager.Instance.PlayRobotDeath();
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
            if (distance <= _distanceThreshold)
            {
                TellAIToHide(obj);
            }
        }
    }
    private void TellAIToHide(GameObject obj)
    {
        AIController aiController = obj.GetComponent<AIController>();
        if (aiController != null)
        {
            if(aiController._currentState is HideState)
            {
                //Not hidden enough, run away!
                aiController.ChangeState(new RunState(aiController));
            }
            else
            {
                aiController.ChangeState(new HideState(aiController));

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