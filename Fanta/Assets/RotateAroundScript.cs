using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundScript : MonoBehaviour
{
    [Range(0,90)] public float speed; 
    public bool isPlayedOnce;
    bool stoping = true;
    public GameObject target;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        isPlayedOnce = true;
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H) && stoping && !ShutDownScript.isShutDown && !CutSceneEvents.isCutScenePlaying)
        {
            cam.enabled = true;
            isPlayedOnce = false;
            if(stoping)
            {
                StartCoroutine(StopRotation());                
            }
            stoping = false;
        }
        if(!isPlayedOnce)
        {
            transform.LookAt(target.transform);
            transform.RotateAround(target.transform.position,Vector3.up,speed*Time.deltaTime);
        }
    }

    IEnumerator StopRotation()
    {
        yield return new WaitForSeconds(7.5f);
        cam.enabled=false;
        isPlayedOnce = true;
    }
}
