using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public DeathState(AIController aiController) : base(aiController) { }

    public override void Enter()
    {
        UIManager.Instance.UpdateEnemyCount(1);
        UIManager.Instance.AddScore(50);
        SpawnManager.Instance.UpdateActiveEnemies(-1);
        aiController.SetIsDead(true);
        aiController.DisableCollider();
        aiController.TellEnemiesToHide();
        aiController.StopMoving();
        aiController.DeathAnimationStart();
        Debug.Log("Enemy Died");
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