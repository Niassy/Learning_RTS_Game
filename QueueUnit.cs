using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class is used for managing 
public class QueueUnit : MonoBehaviour {

    enum UnitName
    {
        Nothing = 0,
        SCV,
        Marine,
        Tank
    };

    UnitName[] m_UnitWaiting = new UnitName[5] ;

    // current active unit being processed
    int m_CurrentActive;


    // position of unit of queue
    int m_CurrentPos;

    int m_MaxQueue = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void registerUnit(UnitName unit)
    {
        if (m_CurrentPos == m_MaxQueue - 1)
            return;

        m_UnitWaiting[m_CurrentPos] = unit;  
        m_CurrentPos++;

        if (m_CurrentPos > m_MaxQueue - 1)
            m_CurrentPos = m_MaxQueue - 1;
       
    }

    // remove finished unit from queue
    // finishd unit is the first unit
    void removeFinishedUnit()
    {
        m_UnitWaiting[m_CurrentActive] = UnitName.Nothing;
        m_CurrentActive++;
    }

}
