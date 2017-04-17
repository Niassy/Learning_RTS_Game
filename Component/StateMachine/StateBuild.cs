using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBuild : State 
{
    Vector3 m_Position;
    Quaternion m_Rotation;

    // a reference to object being constructed

    GameObject m_Building;
    public override void Enter()
    {
        if (!activated)
            return;

        //Debug.Log("I am building");
        GameObject gameWorld = GameObject.FindGameObjectWithTag("GameWorld");
        gameWorld.GetComponent<BuildManager>().m_PrefabBarrack.GetComponent<TimeBuilding>().m_CurrentTime = TimeBuilding.m_Time;

        gameWorld.GetComponent<BuildManager>().buildBarrackInConstruct(gameWorld, m_Position, m_Rotation);

        //m_Building =  gameWorld.GetComponent<BuildManager>().buildBarrack(gameWorld, m_Position, m_Rotation);
        //m_Building.SetActive(false);
        //GetComponent<StateMachine>().changeState(GetComponent<StateIdle>());

      
    }

    public override void Execute()
    {
        if (!activated)
            return;

        //Debug.Log("I am building");

         GameObject gameWorld = GameObject.FindGameObjectWithTag("GameWorld");
         gameWorld.GetComponent<BuildManager>().m_PrefabBarrack.GetComponent<TimeBuilding>().processTime();

       
         float time = gameWorld.GetComponent<BuildManager>().m_PrefabBarrack.GetComponent<TimeBuilding>().m_CurrentTime;
         Debug.Log(time);


         if (time <= 0.0f)
         {
             //Debug.Log("build finished");
             //gameWorld.GetComponent<BuildManager>().buildBarrack(gameWorld, m_Position, m_Rotation);
             //m_Building.SetActive(true);
             GetComponent<StateMachine>().changeState(GetComponent<StateIdle>());

         }
    }

    public override void Exit()
    {
        if (!activated)
            return;
    }

	// Use this for initialization
	
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
