using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkinController : MonoBehaviour
{
    public ShutDownScript shutDownScript;
    CharacterController cc;
    public AudioClip stepAudioClip;
    public AudioSource stepSound;
    public bool hasJumped = false;
    public MovementInput movementInput;
    Animator animator;
    public bool canChange = true;
    public bool switched = true;
    public BatteryBar batteryBar;
    public Renderer[] characterMaterials;

    public Texture2D[] albedoList;
    [ColorUsage(true,true)]
    public Color[] eyeColors;
    public enum EyePosition {normal, happy, angry, sad, dead,victory}
    public EyePosition eyeState;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        characterMaterials = GetComponentsInChildren<Renderer>();
        characterMaterials[10].material.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if(ShutDownScript.isShutDown)
        {
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("shutdown");
        }
        if(cc.isGrounded && cc.velocity.magnitude > 0f && !stepSound.isPlaying && !Input.GetKey(KeyCode.Space) && !ShutDownScript.isShutDown && !CutSceneEvents.isCutScenePlaying)
        {
            stepSound.volume = Random.Range(0.2f,0.3f);
            stepSound.pitch = Random.Range(2f,2.5f);
            stepSound.Play();
        }
        if(Input.GetKeyDown(KeyCode.Space) && (movementInput.InputX!=0 || movementInput.InputZ!=0) && !ShutDownScript.isShutDown)
        {
            ChangeAnimatorIdle("death");
            hasJumped = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) && !ShutDownScript.isShutDown)
        {
            ChangeAnimatorIdle("normal");
        }
        if(batteryBar.timeChange<=0.25f && !ShutDownScript.isShutDown)
        {
            ChangeEyeOffset(EyePosition.sad);
            ChangeAnimatorIdle("sad");
            canChange = false;
            switched = false;
        }
        if(batteryBar.timeChange>0.25f && !switched && !ShutDownScript.isShutDown)
        {
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
            switched = true;
            canChange = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && canChange && !ShutDownScript.isShutDown)
        {
            //ChangeMaterialSettings(0);
            ChangeEyeOffset(EyePosition.normal);
            ChangeAnimatorIdle("normal");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && canChange && !ShutDownScript.isShutDown)
        {
            //ChangeMaterialSettings(1);
            ChangeEyeOffset(EyePosition.angry);
            ChangeAnimatorIdle("angry");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && canChange && !ShutDownScript.isShutDown)
        {
            //ChangeMaterialSettings(2);
            ChangeEyeOffset(EyePosition.happy);
            ChangeAnimatorIdle("happy");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && canChange && !ShutDownScript.isShutDown)
        {
            //ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.sad);
            ChangeAnimatorIdle("sad");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && canChange && !ShutDownScript.isShutDown)
        {
            //ChangeMaterialSettings(3);
            ChangeEyeOffset(EyePosition.dead);
            ChangeAnimatorIdle("sad");
        }
    }

    public void ChangeAnimatorIdle(string trigger)
    {
        animator.SetTrigger(trigger);
    }

    void ChangeMaterialSettings(int index)
    {
        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
                characterMaterials[i].material.SetColor("_EmissionColor", eyeColors[index]);
            else
                characterMaterials[i].material.SetTexture("_MainTex",albedoList[index]);
        }
    }

    public void ChangeEyeOffset(EyePosition pos)
    {
        Vector2 offset = Vector2.zero;

        switch (pos)
        {
            case EyePosition.normal:
                offset = new Vector2(0, 0);
                break;
            case EyePosition.happy:
                offset = new Vector2(.33f, 0);
                break;
            case EyePosition.angry:
                offset = new Vector2(.66f, 0);
                break;
            case EyePosition.sad:
                offset = new Vector2(0.33f, .66f);
                break;
            case EyePosition.dead:
                offset = new Vector2(1,0.66f);
                break;
            default:
                break;
        }

        for (int i = 0; i < characterMaterials.Length; i++)
        {
            if (characterMaterials[i].transform.CompareTag("PlayerEyes"))
            {
                characterMaterials[i].material.mainTextureOffset = offset;
            }
                
        }
    }
}
