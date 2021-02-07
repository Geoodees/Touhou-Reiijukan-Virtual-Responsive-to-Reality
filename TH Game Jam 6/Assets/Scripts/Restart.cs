using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Valve.VR;

public class Restart : MonoBehaviour
{
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean InteractUI;

    void Update()
    {
        if (InteractUI.GetLastStateDown(handType))
        {
            RestartGame();
        }
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Eizi Scene");
    }
}
