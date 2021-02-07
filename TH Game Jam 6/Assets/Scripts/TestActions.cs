using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TestActions : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // check which hand
    public SteamVR_Action_Boolean teleportAction; // logs teleport action
    public SteamVR_Action_Boolean grabAction; // logs grab action

    // Update is called once per frame
    void Update()
    {
        // debug log
        if (GetTeleportDown())
        {
            print("Teleport " + handType);
        }

        if (GetGrab())
        {
            print("Grab " + handType);
        }
    }

    public bool GetTeleportDown() // if teleporter triggered, return true
    {
        return teleportAction.GetStateDown(handType);
    }

    public bool GetGrab() // if grab triggered, return true
    {
        return grabAction.GetState(handType);
    }
}
