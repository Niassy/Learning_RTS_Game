using UnityEngine;
using System.Collections;

public class State_Deposit : State 
{

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
        //Debug.Log("I am going to deposit ");
        GameObject BaseCenter = GameObject.FindGameObjectWithTag("Base");
        if (BaseCenter)
        {
          //  Debug.Log("Base center found!!!");
            GetComponent<Movement>().TargetPosition = BaseCenter.transform.position;
        }
    }

    public override void Execute()
    {
        if (!activated)
            return;
        GameObject BaseCenter = GameObject.FindGameObjectWithTag("Base");
        if (Vector3.Distance(gameObject.transform.position,  BaseCenter.transform.position) < 5.35f)
        {
            //GetComponent<Movement>().stop();
            GetComponent<StateMachine>().changeState(GetComponent<State_GoGather>());

        }
    }

    public override void Exit()
    {
        if (!activated)
            return;
        GetComponent<Movement>().stop(); 
    }
}
