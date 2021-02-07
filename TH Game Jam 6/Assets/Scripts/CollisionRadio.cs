using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRadio : MonoBehaviour
{
    public AudioClip clip1;
    public AudioClip clip2;
    int plnum;
    public AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = clip1;
        source.Play(0);
        plnum = 1;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((collision.transform.name == "BouncingOrb") || (collision.transform.name == "Bullet(Clone)")) || (collision.transform.name == "Staff")) ;
        {
            source.Stop();
            switch (plnum)
            {
                case 1:
                    source.clip = clip1;
                    source.Play(0);
                    plnum = 2;
                    break;
                case 2:
                    source.clip = clip2;
                    source.Play(0);
                    plnum = 1;
                    break;
                
            }
            
        }
    }
}
