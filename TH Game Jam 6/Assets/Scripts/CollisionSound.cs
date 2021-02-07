using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{

    public AudioClip clip;
    AudioSource source;

         void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.name == "BouncingOrb")
        {
            source.PlayOneShot(clip);
        }
    }
}
