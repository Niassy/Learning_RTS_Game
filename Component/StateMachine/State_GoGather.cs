using UnityEngine;
using System.Collections;

public class State_GoGather : State {

    // mineral field where worker go to gather
    GameObject mineralField;
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

        float y = gameObject.transform.position.y;
        Vector3 target = new Vector3(mineralField.transform.position.x,y, mineralField.transform.position.z);

        GetComponent<Movement>().TargetPosition = target;
    }

    public override void Execute()
    {
        if (!activated)
            return;

        if (Vector3.Distance(gameObject.transform.position,mineralField.transform.position) < 1.35f)
        {
            //GetComponent<Movement>().stop();
            GetComponent<StateMachine>().changeState(GetComponent<State_Gather>());
        }

    }

    public override void Exit()
    {
        if (!activated)
            return;
        GetComponent<Movement>().stop(); 
    }

    public GameObject MineralField
    {
        set { mineralField = value; }
        get { return mineralField; }
    }
}
