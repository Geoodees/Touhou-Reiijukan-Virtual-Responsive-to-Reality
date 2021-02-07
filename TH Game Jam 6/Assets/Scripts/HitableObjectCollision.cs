using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using static Ditzelgames.PhysicsHelper;

public class HitableObjectCollision : MonoBehaviour
{
    public GameObject staff;
    public GameObject wall;
    public Rigidbody rb;
    private Vector3 velocity = new Vector3(3f, 4f, 0f);
    public SteamVR_Behaviour_Pose controllerPose;

    public float autoforce = 0.05f;

    private void SetCollidingObject(Collider col)
    {
        Vector3 controllerVelocity = staff.GetComponent<Rigidbody>().velocity;
        Vector3 controllerAngularVelocity = controllerPose.GetAngularVelocity();
        col.gameObject.GetComponent<Rigidbody>().AddForce(col.ClosestPoint(col.gameObject.transform.position) - col.gameObject.transform.position * controllerVelocity.magnitude, ForceMode.Impulse);
            
    }

    // trigger
    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(-Vector3.forward * autoforce, ForceMode.Impulse);
    }

    /*void OnCollisionEnter(Collision c)
    {
        // force is how forcefully the object will fly
        float force = 10000000000000000000;

        //get the device associated with that tracked object (which is how you access buttons and stuff)
        //SteamVR_Controller.Device device = SteamVR_Controller.Input((int)trackedObj.index);

        //get the velocity
        //Vector3 vel = device.velocity;

        Debug.Log(controllerPose.GetAngularVelocity());
        Debug.Log(controllerPose.GetAngularVelocity());


        // If the object collides with staff
        if (c.gameObject.tag == "Staff")
        {
            
            // Calculate Angle Between the collision point and the staff
            Vector3 dir = c.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force.
            //velocity = Vector3.Reflect(dir * force, c.contacts[0].normal);
            //rb.velocity = velocity;
            ApplyForceToReachVelocity(rb, -dir * force, 10);
            //rb.AddForce(-dir * force, ForceMode.Impulse);
        }

        //if gameObject collides with "NorthWall", push South (transform.forward * -force)  
        if (c.gameObject.tag == "NWall")
        {
            ApplyForceToReachVelocity(rb, -transform.forward * force, 10);
            QuaternionToAngularVelocity(Quaternion.AngleAxis(43, Vector3.up));
            AngularVelocityToQuaternion(transform.forward);
            //rb.AddForce(-transform.forward * force, ForceMode.Impulse);
        }
        //if gameObject collides with "SouthWall", push North (transform.forward * force)  
        if (c.gameObject.tag == "SWall")
        {
            ApplyForceToReachVelocity(rb, transform.forward * force, 10);
            //rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }
        //if gameObject collides with "WestWall", push East (transform.right * force)  
        if (c.gameObject.tag == "WWall")
        {
            ApplyForceToReachVelocity(rb, transform.right * force, 10);
            //rb.AddForce(transform.right * force, ForceMode.Impulse);
        }
        //if gameObject collides with "EasthWall", push west (transform.right * -force)  
        if (c.gameObject.tag == "EWall")
        {
            ApplyForceToReachVelocity(rb, -transform.right * force, 10);
            //rb.AddForce(-1 * transform.right * force, ForceMode.Impulse);
        }


    }*/

}