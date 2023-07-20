using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(AIController aiController) : base(aiController) { }

    public override void Enter()
    {
        aiController.DisableCollider();
        aiController.TellEnemiesToHide();
        aiController.StopMoving();
        aiController.DeathAnimationStart();
        aiController.Score += 50;
    }

    public override void Update()
    {
        //Do Nothing, Dead and inactive
    }

    public override void Exit()
    {
        Debug.Log("In exit: Enabling Collider again");
        aiController.EnableCollider();
        aiController.DeathAnimationStop();
    }
}