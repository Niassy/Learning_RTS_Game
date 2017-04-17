using UnityEngine;
using System.Collections;

public class StateIdle : State 
{
    public override void Enter()
    {
        if (!activated)
            return;
    }

    public override void Execute()
    {
        if (!activated)
            return;
    }

    public override void Exit()
    {
        if (!activated)
            return;
    }
}
