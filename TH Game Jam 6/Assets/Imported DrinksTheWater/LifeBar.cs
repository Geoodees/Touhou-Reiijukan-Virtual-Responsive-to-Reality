using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LifeBar : MonoBehaviour //note to self constructors overwrite all contents
{
    
    private float HP ; //using it to make it lighter 
    
    public Image image;
    float HPscaleUnit;
    int HPSwitchCode = 0;
    public GameObject particleObject;
    ParticleSystem sparkles;
    public Sprite HP8, HP7, HP6, HP5, HP4, HP3, HP2, HP1;

    private float waittime = .5f;
    private float elaspedTime = 0;
    private int burstcount = 7;

    // Use this for initialization
    void Start()
    {
        image.sprite = HP8;
        HPSwitchCode = 7;
        sparkles = particleObject.GetComponent<ParticleSystem>();

        var main = sparkles.main;
    }

    // Update is called once per frame
    void Update()
    {
        sparkles = particleObject.GetComponent<ParticleSystem>();
        var main = sparkles.main;
        if (elaspedTime < waittime)
        {
            elaspedTime += Time.deltaTime;
        }
        else {
            elaspedTime = 0;
            main.startSpeed = 100;
            var em = sparkles.emission;
            em.rateOverTime = 10;
        }
        
    }
    public LifeBar()//check if bad practice
    {
        Debug.Log("need argument");
    }
    public void startLife(float startingHP)
    {
        HP = startingHP;
        HPscaleUnit = startingHP / 8;
        
    }

    public void setLife(float hp) {

        HP = hp;
        

        if (HP <= (HPscaleUnit * 7))//it works
        {
            image.sprite = HP7;
            HPSwitchCode = 7;
            getBurst(HPSwitchCode);
            if (HP <= (HPscaleUnit * 6))
            {
                image.sprite = HP6;
                HPSwitchCode = 6;
                getBurst(HPSwitchCode);
                if (HP <= (HPscaleUnit * 5))
                {
                    image.sprite = HP5;
                    HPSwitchCode = 5;
                    getBurst(HPSwitchCode);
                    if (HP <= (HPscaleUnit * 4))
                    {
                        image.sprite = HP4;
                        HPSwitchCode = 4;
                        getBurst(HPSwitchCode);
                        if (HP <= (HPscaleUnit * 3))
                        {
                            image.sprite = HP3;
                            HPSwitchCode = 3;
                            getBurst(HPSwitchCode);
                            if (HP <= (HPscaleUnit * 2))
                            {
                                image.sprite = HP2;
                                HPSwitchCode = 2;
                                getBurst(HPSwitchCode);
                                if (HP <= (HPscaleUnit * 1))
                                {
                                    image.sprite = HP1;
                                    HPSwitchCode = 1;
                                    getBurst(HPSwitchCode);
                                    return;
                                }
                                return;
                            }
                            return;
                        }
                        return;
                    }
                    return;
                }
                return;
            }
            return;
        }
        return;

    }

    public void getBurst(int code) {//counts down to how many bursts left
        if (burstcount == code) {
            sparkles = particleObject.GetComponent<ParticleSystem>();
            var main = sparkles.main;
            main.startSpeed = 1000;
            var em = sparkles.emission;
            em.rateOverTime = 1000;
            burstcount--;
        }

        

    }
    


    public int getHPswitch() {
      
        return HPSwitchCode;
    }





}