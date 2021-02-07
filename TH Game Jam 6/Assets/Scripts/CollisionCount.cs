using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCount : MonoBehaviour
{

    public int countCollisions = 0;
    public int pointvalue = 1;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "BouncingOrb") {
            Score.score += pointvalue;
            //consider playing a sound effect here as well
        }
    }
}
