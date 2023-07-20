using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(AIController aiController) : base(aiController) { }

    private float _hideAfterTime;

    public override void Enter()
    {
        _hideAfterTime = Random.Range(5f, 10f);
        aiController.ResumeMoving();
        aiController.UpdateDestination();
    }

    public override void Update()
    {
        // Determine if ai should hide, if so, go to hide state
        if (aiController.ShouldHide())
        {
            aiController.ChangeState(new HideState(aiController));
        }
        /*
        if (!aiController.ShouldHide())
        {
            _hideAfterTime -= Time.deltaTime;
            if(_hideAfterTime <= 0)
            {
                aiController.UpdateDestination(BarrierManager.Instance.GetClosestBarrier(aiController.transform.position));

            }
        }
        // Check if the enemy is near the closest barrier
        if (aiController.RemainingDistance() < 1f && aiController.ShouldHide() && Time.time >5f)
        {
            // If the enemy is near the closest barrier, hide behind it
            aiController.ChangeState(new HideState(aiController));
        }*/
    }

    public override void Exit()
    {
        // Do nothing
    }



}