using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Score : MonoBehaviour
{
    //shows score
    private float timer;
    public Text scoreText;
    public Text spm;

    public static int score = 0;

    void Update()
    {
        scoreText.text = score.ToString("0");

        timer += Time.deltaTime;

        if (timer > 5f)
        {

            score += 5;

            //We only need to update the text if the score changed.
            spm.text = score.ToString();

            //Reset the timer to 0.
            timer = 0;
        }


    }
}
