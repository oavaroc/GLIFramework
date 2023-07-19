using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    public RunState(AIController aiController) : base(aiController) { }

    public override void Enter()
    {
        // Run to end point
        aiController.MoveTowardsEndPoint();
    }

    public override void Update()
    {
        // Determine if ai should hide, if so, go to hide state
        if (aiController.ShouldHide())
        {
            aiController.ChangeState(new HideState(aiController));
        }
    }

    public override void Exit()
    {
        // Do nothing
    }
}