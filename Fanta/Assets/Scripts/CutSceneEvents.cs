using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEvents : MonoBehaviour
{
    public ShutDownScript shutDownScript;
    public static bool isCutScenePlaying = true;
    public GameObject foundText;
    public GameObject destroyedText;
    public GameObject batteryImage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Playing()
    {
        isCutScenePlaying = false;
        foundText.gameObject.SetActive(true);
        destroyedText.gameObject.SetActive(true);
        batteryImage.gameObject.SetActive(true);
    }
}
