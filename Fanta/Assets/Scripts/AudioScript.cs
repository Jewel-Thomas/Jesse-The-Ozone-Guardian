using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public static float startTime = 120;
    public AudioSource replenishOzone;
    public AudioSource robotAudio;
    public bool isPlaying = false;
    public AudioSource nextRoboAudio;
    public AudioSource letsGetStarted;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Purpose",1);
        Invoke("PlayStart",48.15f);
        InvokeRepeating("PlayAudio",startTime,60);
    }

    // Update is called once per frame
    void Update()
    {
        if(ShutDownScript.isShutDown)
        {
            CancelInvoke();
        }
    }

    void Purpose()
    {
        replenishOzone.Play();
    }
    void PlayStart()
    {
        letsGetStarted.Play();
    }
    void PlayAudio()
    {
        if(!isPlaying)
        {
            robotAudio.Play();
            isPlaying = true;
        }
        else
        {
            nextRoboAudio.Play();
            isPlaying = false;
        }
        
    }
}
