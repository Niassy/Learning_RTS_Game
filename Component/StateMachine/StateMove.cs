using UnityEngine;
using System.Collections;

public class StateMove : State 
{
    //Vector3 m_TargetPos;

    public override void Enter()
    {
        if (!activated)
            return;
       // Debug.Log("state Moving");

        //GetComponent<Movement>().TargetPosition = m_TargetPos;
    }

    public override void Execute()
    {
        if (!activated)
            return;

        //Debug.Log("state Moving");
        if (Vector3.Distance(gameObject.transform.position, GetComponent<Movement>().TargetPosition) < 1.35f)
        {
            GetComponent<Movement>().stop();
            //GetComponent<StateMachine>().changeState(GetComponent<StateIdle>());
          //  Debug.Log("stop moving");
        
           
        }

    }

    public override void Exit()
    {
        if (!activated)
            return;
       // GetComponent<Movement>().stop();
    }

    //public Vector3 TargetPosition
    //{
    //    set { m_TargetPos = value; }
    //    get { return m_TargetPos; }
    //}

}
