using UnityEngine;
using System.Collections;

public class State : MonoBehaviour {

    // determine if a state is activated
    // only activated state are allowed to run
    protected bool activated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        Execute();
	}

    public virtual void Enter()
    {
        if (!activated)
            return;
    }

    public virtual void Execute()
    {
        if (!activated)
            return;
    }

    public virtual void Exit()
    {
        if (!activated)
            return;
    }

    public bool Activated
    {
        set { activated = value; }
        get { return activated; }
    }
}
