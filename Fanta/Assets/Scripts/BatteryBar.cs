using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
    public ShutDownScript shutDownScript;
    public GameObject batteryBar;
    public Image batteryColor;
    public float time;
    public bool playingActivated = false;
    public bool changing = false;
    public float timeChange = 1;
    public float deltaTimeChange = 1000;
    // Start is called before the first frame update
    void Start()
    {
        // AnimateBar();
    }

    // Update is called once per frame
    void Update()
    {
        if(!Player1.isGameVictory)
        {
            timeChange = timeChange - Time.deltaTime/deltaTimeChange;
        }
        if(!CutSceneEvents.isCutScenePlaying && !playingActivated)
        {
            timeChange = 1;
            playingActivated = true;
        }
        AnimateBar(timeChange);
        ColorChanger();
        if(timeChange <= 0)
        {
            ShutDownScript.isShutDown = true;
        }
        
    }

    public void AnimateBar(float change)
    {
        if(timeChange>=0 && !CutSceneEvents.isCutScenePlaying)
        {
            batteryColor.rectTransform.localScale = new Vector3(timeChange,1,1);
        }
        
    }

    public void ColorChanger()
    {
        Color changeColor = Color.Lerp(Color.red,Color.green,batteryColor.rectTransform.localScale.x);
        batteryColor.color = changeColor;
    }
}
