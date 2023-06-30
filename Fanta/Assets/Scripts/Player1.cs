using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player1 : MonoBehaviour
{
    // public Animator animator;
    public TextMeshProUGUI uniqueId;
    public AudioSource allPoweredUp;
    public AudioSource feelBetter;
    public bool playedMorePower = false;
    public AudioSource morePower;
    public AudioSource shootYouDown;
    public GameObject victoryPanel;
    public static bool isGameVictory = false;
    private AudioScript audioScript;
    public AudioSource victorySound;
    public bool wasPlayed = false;
    public CharacterSkinController characterSkinController;
    public GameObject lowBatteryPanel;
    public BatteryBar batteryBar;
    public AudioSource shootAudio;
    private GameObject currentCrystal = null;
    public Material eyeMaterial;
    public GameObject bulletEmitter;
    public GameObject bulletEmitter2;
    public GameObject bullet;
    private Transform crystalPosition = null;
    public AudioSource hereWeGo;
    public AudioSource gotYou;
    public GameObject guns;
    public bool inGunMode = false;
    public Material material;
    public bool isGamePaused = false;
    public GameObject panel;
    public int count = 0;
    public Text text;
    public GameObject bulletPrefab;
    public Camera attackCam;
    public Material crystalMat;
    
    private PlayerController playerController;
    // Start is called before the first frame update
    void Awake()
    {
        audioScript = GetComponent<AudioScript>();
    }
    void Start()
    {
        uniqueId.text = "Unique Id : " + SystemInfo.deviceUniqueIdentifier;
        eyeMaterial.DisableKeyword("_EMISSION");  
        isGameVictory = false; 
        wasPlayed = false; 
        victoryPanel.gameObject.SetActive(false);
        playedMorePower = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DamageScript.destroyed >= 25 && !wasPlayed)
        {
            isGameVictory = true;
            audioScript.CancelInvoke();
            victorySound.Play();
            victoryPanel.gameObject.SetActive(true);
            wasPlayed = true;
            characterSkinController.ChangeAnimatorIdle("victory");
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if(Input.GetKeyDown(KeyCode.Escape) && !ShutDownScript.isShutDown)
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(Input.GetKeyDown(KeyCode.H) && !ShutDownScript.isShutDown && !CutSceneEvents.isCutScenePlaying)
        {
            if(!inGunMode)
            {
                characterSkinController.ChangeEyeOffset(CharacterSkinController.EyePosition.angry);
                characterSkinController.ChangeAnimatorIdle("angry");
                // attackCam.enabled = true;
                // animator.Play("CameraShow"); 
                Takedown();
            }
            else
            {
                characterSkinController.ChangeEyeOffset(CharacterSkinController.EyePosition.normal);
                characterSkinController.ChangeAnimatorIdle("normal");
                Hostler();
            }
        }
        if(inGunMode && ShutDownScript.isShutDown)
        {
            Hostler();
        }
        if(Input.GetButtonDown("Fire1") && inGunMode && !isGamePaused)
        {
            ShootBullets();
        }
        if(batteryBar.timeChange<0.25f)
        {
            Debug.Log("Warning : Low Battery!");
            lowBatteryPanel.gameObject.SetActive(true);
        }
        if(batteryBar.timeChange<0.25f && !playedMorePower && inGunMode && !ShutDownScript.isShutDown)
        {
            morePower.Play();
            playedMorePower = true;
        }
        if(batteryBar.timeChange>0.25f || ShutDownScript.isShutDown)
        {
            lowBatteryPanel.gameObject.SetActive(false);
            playedMorePower = false;
        }
        
    }

    public void Takedown()
    {
        guns.gameObject.SetActive(true);
        inGunMode = true;
        hereWeGo.Play();
        characterSkinController.characterMaterials[10].material.EnableKeyword("_EMISSION");
        batteryBar.deltaTimeChange = 200;
    }
    public void Hostler()
    {
        guns.gameObject.SetActive(false);
        inGunMode = false;
        characterSkinController.characterMaterials[10].material.DisableKeyword("_EMISSION");
        batteryBar.deltaTimeChange = 1000;
    }


    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Crystal"))
        {
            crystalPosition = col.transform;
            currentCrystal = col.gameObject;
            material = col.gameObject.GetComponent<MeshRenderer>().material;
            playerController = col.gameObject.GetComponent<PlayerController>();
            
            material.EnableKeyword("_EMISSION");
            
            if(!playerController.isGlowing)
            {
               playerController.chimingAudio.Play();
               playerController.chimingAudio.loop = true;
               count++;
               text.text = "Crystal Found : " + count;
               if(!inGunMode)
               {
                   gotYou.Play();
               }
               else
               {
                   shootYouDown.Play();
               }
               
            }
            
            playerController.isGlowing = true;
             
        }
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BigBattery"))
        {
            //batteryBar.batteryColor.rectTransform.localScale = new Vector3(1,1,1);
            batteryBar.timeChange = 1;
            batteryBar.AnimateBar(batteryBar.timeChange);
            Destroy(other.gameObject);
            allPoweredUp.Play();
        }
        if(other.gameObject.CompareTag("SmallBattery"))
        {
            if(batteryBar.timeChange < 0.5f)
            {
                batteryBar.timeChange = batteryBar.timeChange + 0.5f;
                batteryBar.AnimateBar(batteryBar.timeChange);
                Destroy(other.gameObject);
                feelBetter.Play();
            }
            else
            {
                batteryBar.timeChange = 1;
                batteryBar.AnimateBar(batteryBar.timeChange);
                Destroy(other.gameObject);
                feelBetter.Play();
            }
        }
    }

    

    void ShootBullets()
    {
        GameObject Temporary_Bullet_Handler1;
        GameObject Temporary_Bullet_Handler2;
        Temporary_Bullet_Handler1 = Instantiate(bullet,bulletEmitter.transform.position,bulletEmitter.transform.rotation) as GameObject;
        Temporary_Bullet_Handler1.transform.Rotate(Vector3.forward*90);
        Temporary_Bullet_Handler2 = Instantiate(bullet,bulletEmitter2.transform.position,bulletEmitter2.transform.rotation) as GameObject;
        Temporary_Bullet_Handler2.transform.Rotate(Vector3.forward*90);
        shootAudio.Play();
        
        Temporary_Bullet_Handler1.GetComponent<BulletBehaviour>().Fire(crystalPosition);
        Temporary_Bullet_Handler2.GetComponent<BulletBehaviour>().Fire(crystalPosition);
        Rigidbody rb = Temporary_Bullet_Handler1.GetComponent<Rigidbody>();
        Rigidbody rb2 = Temporary_Bullet_Handler2.GetComponent<Rigidbody>();
        if(crystalPosition == null)
        {
            rb.AddForce(transform.forward*500,ForceMode.Impulse);
            rb2.AddForce(transform.forward*500,ForceMode.Impulse);
        }   
    }
}
