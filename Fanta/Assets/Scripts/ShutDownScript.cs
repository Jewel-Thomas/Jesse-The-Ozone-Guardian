using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShutDownScript : MonoBehaviour
{
    public GameObject deadPanel;
    public static bool isShutDown = false;
    public AudioSource shutDownAudio;
    public AudioSource bgm;
    public bool hasPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        isShutDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isShutDown && !hasPlayed)
        {
            Debug.Log("ShutDown");
            shutDownAudio.Play();
            hasPlayed = true;
            Invoke("DeadPanelView",10);
        }
        if(isShutDown && bgm.pitch>=0)
        {
            bgm.pitch = bgm.pitch - Time.deltaTime/10;
        }
    }

    void DeadPanelView()
    {
        deadPanel.gameObject.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
