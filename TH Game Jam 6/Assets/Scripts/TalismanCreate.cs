using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class TalismanCreate : MonoBehaviour
{
    public float lifetime = 5.0f;

    public float constantSpeed = 1.0f;
    public float smoothingFactor = 1.0f;

    public GameObject controller;
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;

    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        SetInactive();
    }

    public void Launch()
    {
        transform.position = controller.GetComponent<SteamVR_Behaviour_Pose>().transform.position;
        transform.rotation = controller.GetComponent<SteamVR_Behaviour_Pose>().transform.rotation;

        //transform.GetComponent<Rigidbody>().velocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetVelocity();
        //transform.GetComponent<Rigidbody>().angularVelocity = controller.GetComponent<SteamVR_Behaviour_Pose>().GetAngularVelocity();

        rb.AddRelativeForce(this.transform.forward * 10, ForceMode.Impulse);
        
        StartCoroutine(TrackLifetime());
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GameObject.Find("Controller (left)");
        Launch();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //var cvel = rb.velocity;
        //var tvel = cvel.normalized * constantSpeed;
        //rb.velocity = Vector3.Lerp(cvel, tvel, Time.deltaTime * smoothingFactor);
    }

    private IEnumerator TrackLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        SetInactive();
    }

    public void SetInactive()
    {
        //rb.velocity = Vector3.zero;
        //rb.angularVelocity = Vector3.zero;

        Destroy(gameObject);
    }
}
