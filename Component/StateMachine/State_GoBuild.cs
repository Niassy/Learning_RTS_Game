using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GoBuild : State 
{
    public Vector3 m_Position;
    Quaternion m_Rotation;

    public Vector3 m_OwnerPos;


    public override void Enter()
    {
        if (!activated)
            return;


        Debug.Log("going build");

        float y = gameObject.transform.position.y;
        Vector3 target = new Vector3(m_Position.x, y, m_Position.z);

        m_Position = GetComponent<Movement>().TargetPosition;

        //GetComponent<Movement>().TargetPosition = target;
    }

    public override void Execute()
    {
        if (!activated)
            return;
        m_OwnerPos = gameObject.transform.position;


        //Debug.Log("going build");

        if (Vector3.Distance(gameObject.transform.position, m_Position) < 0.35f)
        {
            //GetComponent<Movement>().stop();
            //GetComponent<StateMachine>().changeState(GetComponent<State_Gather>());

            //Debug.Log("preparing to exit");

            GetComponent<StateBuild>().Position = m_Position;
            GetComponent<StateBuild>().Rotation = Rotation;
            GetComponent<StateMachine>().CurrentState = GetComponent<StateBuild>();
           
        }

    }

    public override void Exit()
    {
        if (!activated)
            return;

        GetComponent<Movement>().stop();
    }

    public Vector3 Position
    {
        set { m_Position = value; }
        get { return m_Position; }
    }

    public Quaternion Rotation
    {
        set { m_Rotation = value; }
        get { return m_Rotation; }
    }

}

