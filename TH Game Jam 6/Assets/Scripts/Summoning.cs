using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Summoning : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean snapturnright;

    public Transform lefthand;
    public GameObject o;

    void Update()
    {
        if (snapturnright.GetLastStateDown(handType))
        {
            summon();
        }
    }

    public void summon()
    {
        o.transform.position = lefthand.transform.position;
    }
}
