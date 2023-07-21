using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideState : State
{
    private float _hideDuration;
    private float _hideTimer;

    public HideState(AIController aiController) : base(aiController)
    {
        _hideDuration = Random.Range(2f, 5f);
    }

    public override void Enter()
    {
        if (aiController.GetNeverFear())
        {

            aiController.ChangeState(new RunState(aiController));
        }
        else
        {
            Debug.Log("Entering Hide State " );
            // When entering Hide state, find nearest hiding place in front of them, run there
            aiController.StopMoving();
            aiController.HideAnimationStart();
            aiController.ShrinkCollider();
            _hideTimer = 0f;

        }
    }

    public override void Update()
    {
        //Debug.Log("_hideTimer : "+ _hideTimer + " / _hideDuration : " + _hideDuration);
        // hide for a time when at the hiding spot, then go to run state
        _hideTimer += Time.deltaTime;
        if (_hideTimer >= _hideDuration)
        {
            aiController.ChangeState(new RunState(aiController));
        }
    }

    public override void Exit()
    {
        aiController.HideAnimationStop();
        aiController.ExpandCollider();
        aiController.HideComplete();
    }
}