using UnityEngine;
using System.Collections;

public class StateMachine : MonoBehaviour {

    // reference to current state running
    State m_pCurrentState;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void changeState(State state)
    {
        m_pCurrentState.Exit();
        m_pCurrentState.Activated = false;
        CurrentState = state;

        //m_pCurrentState = state;
        //m_pCurrentState.Activated = true;
    }

    public State CurrentState
    {
        set { 
               if (m_pCurrentState)
                   m_pCurrentState.Activated = false;
               m_pCurrentState = value; 
               m_pCurrentState.Activated = true;
               m_pCurrentState.Enter(); 
            }

        get { return m_pCurrentState; }
    }
}
