using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected AIController aiController;

    public State(AIController aiController)
    {
        this.aiController = aiController;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}
