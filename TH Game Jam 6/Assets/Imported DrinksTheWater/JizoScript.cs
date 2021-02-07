using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JizoScript : MonoBehaviour
{

    Transform target;
    public AudioSource audioSource;
    public AudioClip sound;

    public SpriteRenderer image;
    public Sprite JizuOn, JizoOff;

    ParticleSystem sparkles;

    public float triggerDistance = 5.0f;
    bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        sparkles = GetComponent<ParticleSystem>();
        sparkles.Stop();
        //target = PlayerManager.instance.player.transform;
        image.sprite = JizoOff;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        FaceTarget();
        if (distance <= triggerDistance && !(activated)) {
            activated = activate();
        }

    }

    public bool activate() {
        sparkles = GetComponent<ParticleSystem>();
        sparkles.Play();
        audioSource.PlayOneShot(sound, 0.5f);
        image.sprite = JizuOn;
        
        image.color = new Color(.9f, .8f, .8f, 1);
        return true;
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; //sets direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); //location to point
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 13f);  //slerp makes the turning smooth
    }

    void FaceTargetSnap()
    {
        Vector3 direction = (target.position - transform.position).normalized; //sets direction
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); //location to point
        transform.rotation = lookRotation;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, triggerDistance);
        

    }
}
