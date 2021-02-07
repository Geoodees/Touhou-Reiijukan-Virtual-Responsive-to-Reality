using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class GrabObject : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean grabAction;

    private GameObject collidingObject; // store any object you collide with
    private GameObject objectInHand; // store any object currently grabbed

    private void SetCollidingObject(Collider col)
    {
        // check if we already holding
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
        // set object as potential to be grabbable
        collidingObject = col.gameObject;
    }

    // trigger to start checking if nearby object grabbable
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    // trigger, but in case we are already in object (so it doesnt just check once)
    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
    }

    // remove reference to object we could hold when we leave it
    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }

        collidingObject = null;
    }

    private void Grab()
    {
        // move the object to hand
        objectInHand = collidingObject;
        collidingObject = null;
        // connect it to your hand, using a joint
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    // function to add the joint
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {
        // only run if there is a joint between hand and object
        if (GetComponent<FixedJoint>())
        {
            // remove the joint
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
            // add any phystics from the controller onto the object, so it is throwable
            objectInHand.GetComponent<Rigidbody>().velocity = controllerPose.GetVelocity();
            objectInHand.GetComponent<Rigidbody>().angularVelocity = controllerPose.GetAngularVelocity();

        }
        // remove reference
        objectInHand = null;
    }


    // Update is called once per frame
    void Update()
    {
        // 1
        if (grabAction.GetLastStateDown(handType))
        {
            if (collidingObject)
            {
                Grab();
            }
        }

        // 2
        if (grabAction.GetLastStateUp(handType))
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
