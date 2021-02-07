using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Yarn.Unity;

public class LaserPointer : MonoBehaviour
{
    //Teleport stuff
    public Transform cameraRigTransform;
    public GameObject teleportReticlePrefab;
    public GameObject talkReticlePrefab;
    private GameObject teleReticle;
    private GameObject talkReticle;
    private Transform teleportReticleTransform;
    private Transform talkReticleTransform;
    public Transform headTransform;
    public Vector3 teleportReticleOffset;
    public LayerMask teleportMask;
    public LayerMask talkMask;
    private bool shouldTeleport;
    private bool shouldTalk;

    // Laser Stuff
    public SteamVR_Input_Sources handType;
    public SteamVR_Behaviour_Pose controllerPose;
    public SteamVR_Action_Boolean teleportAction;
    public SteamVR_Action_Boolean talkAction;

    public GameObject laserPrefab;
    private GameObject laser;
    private Transform laserTransform;
    private Vector3 hitPoint;
    private string currentCharacter;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn laser
        laser = Instantiate(laserPrefab);
        // Store laser position
        laserTransform = laser.transform;
        // Spawn rectile
        teleReticle = Instantiate(teleportReticlePrefab);
        talkReticle = Instantiate(talkReticlePrefab);
        // Store rectile pos
        teleportReticleTransform = teleReticle.transform;
        talkReticleTransform = talkReticle.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportAction.GetState(handType))
        {
            RaycastHit hit;

            // shoot a raycast to see if we hit something to point at
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                shouldTalk = false;
                talkReticle.SetActive(false);
                hitPoint = hit.point;
                ShowLaser(hit);
                // We can now show a rectile to show where you can teleport
                teleReticle.SetActive(true);
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTeleport = true;
            }
            // shoot a raycast to see if we hit something to point at
            if (Physics.Raycast(controllerPose.transform.position, transform.forward, out hit, 100, talkMask))
            {
                shouldTeleport = false;
                teleReticle.SetActive(false);
                hitPoint = hit.point;
                ShowLaser(hit);
                // We can now show a rectile show that you can talk
                talkReticle.SetActive(true);
                talkReticleTransform.position = hitPoint + teleportReticleOffset;
                shouldTalk = true;
                currentCharacter = hit.collider.tag;
            }
        }
        else if (teleportAction.GetStateUp(handType) && shouldTeleport)
        {
            Teleport();
        }
        else if (teleportAction.GetStateUp(handType) && shouldTalk)
        {
            Talk(currentCharacter);
        }
        else // if no hit, no need to show laser or rectile
        {
            laser.SetActive(false);
            teleReticle.SetActive(false);
        }
    }

    private void ShowLaser(RaycastHit hit)
    {
        // show laser
        laser.SetActive(true);
        // position the laser where the raycast goes, using lerp
        laserTransform.position = Vector3.Lerp(controllerPose.transform.position, hitPoint, .5f);
        // make the laser rotate to the hitpos
        laserTransform.LookAt(hitPoint);
        // scale the laser based on distance
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
                                                laserTransform.localScale.y,
                                                hit.distance);
    }

    private void Teleport()
    {
        // make sure you cannot teleport in a teleport
        shouldTeleport = false;
        teleReticle.SetActive(false);
        // Calculate where the camera should be placed
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        difference.y = 0;
        cameraRigTransform.position = hitPoint + difference;
    }

    private void Talk(string currentCharacter)
    {
        shouldTalk = false;
        talkReticle.SetActive(false);

        Debug.Log("Start talking");
        FindObjectOfType<DialogueRunner>().StartDialogue(currentCharacter);
    }

    /* public void CheckForNearbyNPC ()
        {
            var allParticipants = new List<NPC> (FindObjectsOfType<NPC> ());
            var target = allParticipants.Find (delegate (NPC p) {
                return string.IsNullOrEmpty (p.talkToNode) == false && // has a conversation node?
                (p.transform.position - this.transform.position)// is in range?
                .magnitude <= interactionRadius;
            });
            if (target != null) {
                // Kick off the dialogue at this node.
                FindObjectOfType<DialogueRunner> ().StartDialogue (target.talkToNode);
            }
        }*/
}
