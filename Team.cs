using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team : MonoBehaviour {

    // 2nd list entity
    [HideInInspector]
    private List<GameObject> m_Members = new List<GameObject>();

    public int m_ID;

    // team color
    public Color m_TeamColor;  

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

        //print("bonjour");
	}


    // search for target
    //On this version weapon are assigned at same target
    // at each search
    public void searchforTarget(Team other)
    {
        //print("bonjour");
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3( 200 ,transform.position.y , transform.position.z), 5.0f * Time.deltaTime);
        if( other == null || other == this)
        {
            return;
        }

        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(20, transform.position.y, transform.position.z), 5.0f * Time.deltaTime);

        //transform.position = new Vector3(20, transform.position.y, transform.position.z);
        
        // get all element on team
        foreach (GameObject ally in m_Members)
        {
            if (!ally.activeSelf)
                continue;

            //transform.position = new Vector3(20, transform.position.y, transform.position.z);
       
            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            Vector3 position = new Vector3(200, ally.transform.position.y,300);
            //ally.transform.position = Vector3.MoveTowards(ally.transform.position, position, 5.0f * Time.deltaTime);
            foreach (GameObject ennemy in other.m_Members)
            {
                //transform.position = new Vector3(20, transform.position.y, transform.position.z);

                //ally.transform.position = Vector3.MoveTowards(ally.transform.position, new Vector3(20, ally.transform.position.y, ally.transform.position.z),5.0f * Time.deltaTime );
                
                if (ennemy.activeSelf == true)
                   targetSystem.Target = ennemy;
                //Vector3 position = new Vector3(200, ally.transform.position.y,300);
                //ally.transform.position = Vector3.MoveTowards(ally.transform.position, position, 5.0f * Time.deltaTime);
               
            }
        }
    }

    // On this version we use MMR ALGO
    // and modele mathematic (1)
    // ie weapon try to destroy unit with
    // low health
    public void assignTarget(Team other)
    {
        if (other == null || other == this)
        {
            return;
        }

        int sizeTeam = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            if (ennemy.activeSelf)
                sizeTeam++;
        }

        if (sizeTeam == 0)
            return;

        // v
        GameObject[] tabEnnemy = new GameObject[ sizeTeam /*other.m_Members.Capacity*/]; 
        float[] H = new float[ sizeTeam /*other.m_Members.Capacity*/];
        float[] hl = new float[ sizeTeam /*other.m_Members.Capacity*/];
        int i = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            if (ennemy.activeSelf)
            {
                tabEnnemy[i] = ennemy;
                H[i] = ennemy.GetComponent<TankHealth>().Health;
                hl[i] = H[i];
                i++;
            }
        }

        foreach (GameObject ally in m_Members)
        {
            if (!ally.activeSelf)
                continue;

            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            float minValue = 100000;
            int allocated = -1;
            float dmg = ally.GetComponent<TankShooting>().damage;
            for (int t = 0; t < tabEnnemy.Length; t++ )
            {

               // if (tabEnnemy[t].activeSelf == false)
                 //   continue;

                float reduct =  ( hl[t] - dmg ) / H[t];

                if (hl[t] <= 0)
                    continue;

                if (minValue > reduct)
                {
                    minValue = reduct;
                    allocated = t;
                }
            }
            if (allocated != -1)
            {
                targetSystem.Target = tabEnnemy[allocated];
                hl[allocated] -= dmg;
            }

            /*foreach (GameObject ennemy in other.m_Members)
            {
                targetSystem.Target = ennemy;
                //float dj
                float value = ally.GetComponent<TankShooting>().damage
            }*/
        }
    }

    // On this version we use MMR ALGO
    // and modele mathematic (1)
    // combined to factor of damage
    // ie IA prefers destroy power unit damage
    public void assignTarget1(Team other)
    {
        if (other == null || other == this)
        {
            return;
        }

        int sizeTeam = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            if (ennemy.activeSelf)
                sizeTeam++;
        }

        if (sizeTeam == 0)
            return;

        // v
        GameObject[] tabEnnemy = new GameObject[sizeTeam /*other.m_Members.Capacity*/];
        float[] H = new float[sizeTeam /*other.m_Members.Capacity*/];
        float[] hl = new float[sizeTeam /*other.m_Members.Capacity*/];
        int i = 0;
        float TotalDamageEnnemy = 0;
        float TotalHealthEnnemy = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            if (ennemy.activeSelf)
            {
                tabEnnemy[i] = ennemy;
                H[i] = ennemy.GetComponent<TankHealth>().Health;
                hl[i] = H[i];
                TotalDamageEnnemy += tabEnnemy[i].GetComponent<TankShooting>().damage;
                TotalHealthEnnemy += tabEnnemy[i].GetComponent<TankHealth>().Health;
                i++;
            }
        }

        int w =0;
        foreach (GameObject ally in m_Members)
        {
            if (!ally.activeSelf)
                continue;

            w++;

            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
           // if (targetSystem.Target != null && targetSystem.Target.activeSelf)
             // continue;

            // 
            float minValue = 100000;

            // ratio between damage and Health
            float maxRedDamage = -100000.0f;

            // 
            float maxRatioDmgWT = -100000.0f;

            // get ratio damage weapon target of allocated
            float ratioDmgWTAllocated =  -10000.0f;

            int allocated = -1;
            float dmg = ally.GetComponent<TankShooting>().damage;
            for (int t = 0; t < tabEnnemy.Length; t++)
            {

                if (hl[t] <= 0)
                {
                    continue;
                }
                float dmg1 = tabEnnemy[t].GetComponent<TankShooting>().damage;
                float reduct = (hl[t] - dmg) / H[t];

                // ratio reduction damage on team
                float ratioReductDmg = 1 - ((TotalDamageEnnemy - dmg1) / TotalDamageEnnemy);

                // ratio reduction Health of target
                float ratioReductH = 1 - ((TotalHealthEnnemy - dmg) / TotalHealthEnnemy)     /*reduct*/;

                // Ration beetwen damage and health
                float ratioDmgH = ratioReductDmg / ratioReductH;

                // ratio between weapon's damage and target's damage
                float ratioDmgWTCurrent = dmg1 / dmg;

                // true if current target is assigned
                bool alreadyAllocated = false;

                //print("W =" + w + " T = " + t + " dmgW = " + dmg + " dmgT = " + dmg1 + " ratio1 = " + ratioDmgWTCurrent + " ratio2 = " + ratioDmgWTAllocated);
                 
                if ( ratioDmgWTCurrent / ratioDmgWTAllocated  >= 1.0f)
                {
                    if (ratioDmgWTCurrent / ratioDmgWTAllocated >= /*10.0f*/ 5.0f)  
                    {
                        alreadyAllocated = true;
                        if (ratioDmgWTCurrent > maxRatioDmgWT)
                        {
                            ratioDmgWTAllocated = ratioDmgWTCurrent;
                            maxRatioDmgWT = ratioDmgWTCurrent;
                            allocated = t;
                            //alreadyAllocated = true;
                        }
                    }
                }

                else
                {
                    if (ratioDmgWTAllocated / ratioDmgWTCurrent >= /* 10.0f*/5.0f)
                    {
                        alreadyAllocated = true;
                    }

                    else
                    {
                        //print("damage current" +dmg);
                        //if (allocated != -1)
                          //print("dmg alloc" + tabEnnemy[allocated].GetComponent<TankShooting>().damage);
                    }
                }

                if (minValue > reduct)
                {
                    minValue = reduct;
                    if (alreadyAllocated == false)
                    {
                        ratioDmgWTAllocated = ratioDmgWTCurrent;
                        allocated = t;
                    }
                }
            }

            if (allocated != -1)
            {
                targetSystem.Target = tabEnnemy[allocated];
                hl[allocated] -= dmg;
                ratioDmgWTAllocated = tabEnnemy[allocated].GetComponent<TankShooting>().damage / dmg;
                //print("---------------------Allocation-----------------");
                //print("W =" + w + " T = " + allocated + " dmgW = " + dmg + " dmgT = " + tabEnnemy[allocated].GetComponent<TankShooting>().damage);
                //print("----------------------Fin--------------------------");
            }
        }
    }


    // On this version we use MMR ALGO
    // and modele mathematic (1)
    // combined to factor of damage
    // ie IA prefers destroy power unit damage
    //public void assignTarget1(Team other)
    //{
    //    if (other == null || other == this)
    //    {
    //        return;
    //    }

    //    int sizeTeam = 0;
    //    foreach (GameObject ennemy in other.m_Members)
    //    {
    //        if (ennemy.activeSelf)
    //            sizeTeam++;
    //    }

    //    if (sizeTeam == 0)
    //        return;

    //    // v
    //    GameObject[] tabEnnemy = new GameObject[sizeTeam /*other.m_Members.Capacity*/];
    //    float[] H = new float[sizeTeam /*other.m_Members.Capacity*/];
    //    float[] hl = new float[sizeTeam /*other.m_Members.Capacity*/];
    //    int i = 0;
    //    float TotalDamageEnnemy = 0;
    //    float TotalHealthEnnemy = 0;
    //    foreach (GameObject ennemy in other.m_Members)
    //    {
    //        if (ennemy.activeSelf)
    //        {
    //            tabEnnemy[i] = ennemy;
    //            H[i] = ennemy.GetComponent<TankHealth>().Health;
    //            hl[i] = H[i];
    //            TotalDamageEnnemy += tabEnnemy[i].GetComponent<TankShooting>().damage;
    //            TotalHealthEnnemy += tabEnnemy[i].GetComponent<TankHealth>().Health;
    //            i++;
    //        }
    //    }

    //    foreach (GameObject ally in m_Members)
    //    {
    //        if (!ally.activeSelf)
    //            continue;

    //        TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
    //        //if (targetSystem.Target != null && targetSystem.Target.activeSelf)
    //          //  continue;

    //        // 
    //        float minValue = 100000;

    //        // ratio between damage and Health
    //        float maxRedDamage = -100000.0f;

    //        // 
    //        float maxRatioDmgWT = 0.0f;

    //        int allocated = -1;
    //        float dmg = ally.GetComponent<TankShooting>().damage;
    //        for (int t = 0; t < tabEnnemy.Length; t++)
    //        {
    //            float dmg1 = tabEnnemy[t].GetComponent<TankShooting>().damage;

    //            float reduct = (hl[t] - dmg) / H[t];

    //            // ratio reduction damage on team
    //            float ratioReductDmg = 1 - ( (TotalDamageEnnemy - dmg1 ) / TotalDamageEnnemy); 

    //            // ratio reduction Health of target
    //            float ratioReductH = 1 - ((TotalHealthEnnemy - dmg) / TotalHealthEnnemy)     /*reduct*/;

    //            if (hl[t] <= 0)
    //                continue;

    //            // Ration beetwen damage and health
    //            float ratioDmgH = ratioReductDmg  / ratioReductH;

    //            // ratio between weapon's damage and target's damage
    //            float ratioDmgWT = dmg / dmg1;



    //            /*if (ratioReductDmg > maxRedDamage)
    //            {
    //                maxRedDamage = ratioReductDmg;
    //                allocated = t;
    //            }*/

    //            if (ratioReductDmg < ratioReductH && ratioDmgH >= 0.5f)  // 0.08
    //            {
    //                /* print("total " + TotalDamageEnnemy);
    //                 print("ratio damage" + ratioReductDmg);
    //                 print("ratio Vie" + ratioReductH);
    //                 print("dmg = " + dmg1);*/

    //                if (maxRedDamage < ratioDmgH)
    //                {
    //                    maxRedDamage = ratioDmgH;
    //                    allocated = t;
    //                }

    //                if (minValue > reduct)
    //                    minValue = reduct;
    //            }




    //            else if (minValue > reduct)
    //            {
    //                minValue = reduct;
    //                allocated = t;
    //            }
    //        }

    //        if (allocated != -1)
    //        {
    //            targetSystem.Target = tabEnnemy[allocated];
    //            hl[allocated] -= dmg;
    //        }

    //        /*foreach (GameObject ennemy in other.m_Members)
    //        {
    //            targetSystem.Target = ennemy;
    //            //float dj
    //            float value = ally.GetComponent<TankShooting>().damage
    //        }*/
    //    }
    //}


    // weapon are assigned to target randomly
    public void assignTargetRandomly(Team other)
    {
         if (other == null || other == this)
         {
            return;
         }

        int sizeTeam = 0;
        foreach (GameObject ennemy in other.m_Members)
        {
            if (ennemy.activeSelf)
                sizeTeam++;
        }

        if (sizeTeam == 0)
            return;

        GameObject[] tabEnnemy = new GameObject[ sizeTeam /*other.m_Members.Capacity*/ ];
         int i = 0;
         foreach (GameObject ennemy in other.m_Members)
         {
            if (ennemy.activeSelf)
            {
                tabEnnemy[i] = ennemy;
                i++;
            }
         }


        // get all element on team
        foreach (GameObject ally in m_Members)
        {
            if (!ally.activeSelf)
                continue;

            TargetSystem targetSystem = ally.GetComponent<TargetSystem>();
            if (targetSystem.Target != null && targetSystem.Target.activeSelf)
                continue;

            Vector3 position = new Vector3(200, ally.transform.position.y, 300);
            int count = 0;
            GameObject ennemy = null;
            while (count <= sizeTeam  /*tabEnnemy.Length*/  )
            {
                int randomID = Random.Range(0, sizeTeam /*tabEnnemy.Length*/);
                ennemy= tabEnnemy[randomID];
                if ( ennemy && ennemy.activeSelf)
                    break;
                count++;
            }

            if ( ennemy!= null && ennemy.activeSelf== true)
             {
                 targetSystem.Target = ennemy;
             }
            
        }
    }

    public void addMember(GameObject member)
    {
        m_Members.Add(member);
    }

    public List<GameObject> Members
    {
        get { return m_Members; }
    }

    public List<GameObject> getActiveMembers()
    {
        List<GameObject> members = new List<GameObject>();
        foreach (GameObject ally in m_Members)
        {
            if (ally.activeSelf)
            {
                members.Add(ally);
            }
        }
        return members;
        
    }

    ///////////////////////// Utils function /////////////
    public GameObject[] getArrayActiveMembers()
    {
        int sizeTeam = 0;
        foreach (GameObject ennemy in m_Members)
        {
            if (ennemy.activeSelf)
            {
                sizeTeam++;
            }
        }

        if (sizeTeam == 0)
            return null;

        GameObject[] tabEnnemy = new GameObject[sizeTeam /*other.m_Members.Capacity*/ ];
        int i = 0;
        foreach (GameObject ennemy in m_Members)
        {
            if (ennemy.activeSelf)
            {
                tabEnnemy[i] = ennemy;
                i++;
            }
        }

        return tabEnnemy;
    }

}
