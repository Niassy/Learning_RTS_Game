using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBuilding : MonoBehaviour {

    public static float m_Time = 10.0f;
    public float m_CurrentTime = 1000.0f;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void processTime()
    {
        m_CurrentTime -= Time.deltaTime;
        //Debug.Log(m_CurrentTime);
        if (m_CurrentTime <= 0.0f)
        {
            m_CurrentTime = 0.0f;
        }
    }
}
