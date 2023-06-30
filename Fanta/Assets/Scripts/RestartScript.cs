using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScript : MonoBehaviour
{
    public AudioScript audioScript;
    public CutSceneEvents cutSceneEvents;
    public GameObject director;
    public static bool restarted = false;
    public ShutDownScript shutDownScript;
    public GameObject jesse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(restarted)
        {
            director.gameObject.SetActive(false);
            cutSceneEvents.foundText.gameObject.SetActive(true);
            cutSceneEvents.destroyedText.gameObject.SetActive(true);
            cutSceneEvents.batteryImage.gameObject.SetActive(true);
            audioScript.CancelInvoke("PlayStart");
            audioScript.CancelInvoke("Purpose");
            AudioScript.startTime = 30;

        }
    }

    public void Restart()
    {
       SceneManager.LoadScene(1);
       restarted = true;
       ShutDownScript.isShutDown = false;
    }
}
