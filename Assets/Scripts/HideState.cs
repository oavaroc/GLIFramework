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
        // When entering Hide state, find nearest hiding place in front of them, run there
        aiController.StopMoving();
        _hideTimer = 0f;
    }

    public override void Update()
    {
        // hide for a time when at the hiding spot, then go to run state
        _hideTimer += Time.deltaTime;
        if (_hideTimer >= _hideDuration)
        {
            aiController.ChangeState(new RunState(aiController));
        }
    }

    public override void Exit()
    {
        // Do nothing
    }
}