using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISkip
{
    public void Skip();
}
public class CutSceneEvents : MonoBehaviour,ISkip
{
    [Space]
    [Header("Cutscene Reference")]
    [Tooltip("Refers to the gameobject that deals with cutscenes")]
    [SerializeField] GameObject director;
    [Space]
    [Header("UI")]
    [Tooltip("Reference to the skip button")]
    [SerializeField] GameObject skipButton; 
    [Tooltip("Reference to the foundText UI Gameobject")]
    public GameObject foundText;
    [Tooltip("Reference to the destroyedText UI Gameobject")]
    public GameObject destroyedText;
    [Tooltip("Reference to the Battery UI Gameobject")]
    public GameObject batteryImage;
    public static bool isCutScenePlaying = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && isCutScenePlaying)
        {
            DetectSkippingNeed();
        }
    }
    public void Playing()
    {
        isCutScenePlaying = false;
        foundText.gameObject.SetActive(true);
        destroyedText.gameObject.SetActive(true);
        batteryImage.gameObject.SetActive(true);
    }
    void DetectSkippingNeed()
    {
        Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
        skipButton.SetActive(true);
    }
    public void Skip()
    {
        director.SetActive(false);
        Playing();
        Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
    }
}
