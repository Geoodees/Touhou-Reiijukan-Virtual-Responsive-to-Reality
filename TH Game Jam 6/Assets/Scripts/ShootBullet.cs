using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ShootBullet : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean fireAction;
    public Transform bullet;

    public int Force = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fireAction.GetLastStateDown(handType))
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
        }
    }
}
