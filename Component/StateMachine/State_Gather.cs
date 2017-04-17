using UnityEngine;
using System.Collections;

public class State_Gather : State {

    private float TimeForGather = 2.0f;
    private float m_CurrentTimeGather;
    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public override void Enter()
    {
        if (!activated)
            return;

        m_CurrentTimeGather = TimeForGather;
        Debug.Log("I am gathering");
    }

    public override void Execute()
    {
        if (!activated)
            return;
        m_CurrentTimeGather -= Time.deltaTime;
        if (m_CurrentTimeGather <= 0)
        {
            GetComponent<StateMachine>().changeState(GetComponent<State_Deposit>());
        }
    }

    public override void Exit()
    {
        if (!activated)
            return;
    }

}
