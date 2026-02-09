using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour,ISkip
{
    public AudioSource replenishOzone;
    public AudioSource robotAudio;
    public AudioSource nextRoboAudio;
    public AudioSource letsGetStarted;
    public GameObject skipButton;
    public static float startTime = 120;
    public bool isPlaying = false;
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
        skipButton.SetActive(false);
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
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
    public void Skip()
    {
        CancelInvoke("PlayStart");
        replenishOzone.Stop();
        PlayStart();
    }
}
