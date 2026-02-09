using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player1 : MonoBehaviour
{
    [Space]
    [Header("Audio References")]
    [Tooltip("Audio played when the Jesse picks up a big battery")]
    [SerializeField] AudioSource allPoweredUp;
    [Tooltip("Audio played when the Jesse picks up a small battery")]
    [SerializeField] AudioSource feelBetter;
    private bool playedMorePower = false;
    [Tooltip("Edge case audio where Jesse uses Gun Mode even when the battery is less than 25%")]
    [SerializeField] AudioSource morePower;
    [Tooltip("This is played when Jesse finds and activates a crystal while in gun mode")]
    [SerializeField] AudioSource shootYouDown;
    [Tooltip("This is played when Jesse finds and activates a crystal while not in gun mode")]
    [SerializeField] AudioSource gotYou;
    [Tooltip("Plays when Jesse is toggled to gun mode")]
    [SerializeField] AudioSource hereWeGo;
    [Tooltip("Plays when bullet is shot once")]
    [SerializeField] AudioSource shootAudio;
    [Tooltip("Played once Jesse collects all the crystals and congratulates the player")]
    [SerializeField] AudioSource victorySound;
    [Space]
    [Header("Gameobject References")]
    [Tooltip("Pops in a panel when the battery goes below 25%")]
    [SerializeField] GameObject lowBatteryPanel;
    [Tooltip("Pops in a victory panel after Jesse collects 25 crystals")]
    [SerializeField] GameObject victoryPanel;
    private GameObject currentCrystal = null;
    [Tooltip("Gets the position of the bullets to spawn and shoot it at a direction")]
    public List<GameObject> bullets;
    [Tooltip("Prefab referece of a bullet")]
    public GameObject bullet;
    [Tooltip("Referece for activation/deactivating the gun gameobject")]
    public GameObject guns;
    [Tooltip("Pops in a panel when the game is in pause mode")]
    public GameObject panel;
    [Space]
    [Header("Script References")]
    [Tooltip("Scipt that controls the character Animations and eye positions")]
    [SerializeField] CharacterSkinController characterSkinController;
    [Tooltip("Script that deals with the battery logic")]
    [SerializeField] BatteryBar batteryBar;
    private AudioScript audioScript;
    [Space]
    [Header("Text References")]
    [Tooltip("Gives out a unique Identifier of the computer to avoid plagiarism")]
    [SerializeField] TextMeshProUGUI uniqueId;
    [Tooltip("Displays the number of crystals found")]
    [SerializeField] Text text;
    [Space]
    [Header("Material Refereces")]
    [Tooltip("Changes the eye material property when needed")]
    public Material eyeMaterial;
    [Tooltip("Changes crystal material property when needed")]
    public Material material;
    public static bool isGameVictory = false;
    private bool wasPlayed = false;
    private Transform crystalPosition = null;
    private bool inGunMode = false;
    public bool isGamePaused = false;
    private int count = 0;
    
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
        VictoryLogic();
        PauseMenu();
        AttackMode();
        // Shoots bullet only when the LMB is clicked and in gun-mode and also when the game is not paused
        if(Input.GetButtonDown("Fire1") && inGunMode && !isGamePaused)
        {
            ShootBullets();
        }
        // Minor details that may cover edge cases
        Miscellaneous();
    }

    public void Takedown()
    {
        // Jesse becomes ready to shoot, changes his material properties and changes the increases the battery depletion rate 
        guns.gameObject.SetActive(true);
        inGunMode = true;
        hereWeGo.Play();
        characterSkinController.characterMaterials[10].material.EnableKeyword("_EMISSION");
        batteryBar.deltaTimeChange = 200;
    }
    public void Hostler()
    {
        // Jesse gets to the normal mode, changes his material properties back to normal and decreases the battery depletion rate
        guns.gameObject.SetActive(false);
        inGunMode = false;
        characterSkinController.characterMaterials[10].material.DisableKeyword("_EMISSION");
        batteryBar.deltaTimeChange = 1000;
    }


    public void Pause()
    {
        // Pauses the game and brings in a pause menu screen
        panel.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
        if(!CutSceneEvents.isCutScenePlaying)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Resume()
    {
        // Resumes the game and removes the pause menu screen
        panel.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
        if(!CutSceneEvents.isCutScenePlaying)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void PauseMenu()
    {
        // Toggling between the pause menu and resuming the game
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
    }

    void AttackMode()
    {
        // Getting Jesse to activate and deactivate his Gun-Mode all while checking whether the game is over or the cutscene is playing
        // Also changing the animation states
        if(Input.GetKeyDown(KeyCode.H) && !ShutDownScript.isShutDown && !CutSceneEvents.isCutScenePlaying)
        {
            if(!inGunMode)
            {
                characterSkinController.ChangeEyeOffset(CharacterSkinController.EyePosition.angry);
                characterSkinController.ChangeAnimatorIdle("angry");
                Takedown();
            }
            else
            {
                characterSkinController.ChangeEyeOffset(CharacterSkinController.EyePosition.normal);
                characterSkinController.ChangeAnimatorIdle("normal");
                Hostler();
            }
        }
    }

    // Shoots the bullets from both of the guns when an input is made
    void ShootBullets()
    {
        foreach (var bul in bullets)
        {
            Transform bulTrans = bul.gameObject.transform;
            GameObject Temporary_Bullet_Handler1;
            Temporary_Bullet_Handler1 = Instantiate(bullet,bulTrans.position,bulTrans.rotation) as GameObject;
            Temporary_Bullet_Handler1.transform.Rotate(Vector3.forward*90);
            Temporary_Bullet_Handler1.GetComponent<BulletBehaviour>().Fire(crystalPosition);
            Rigidbody rb = Temporary_Bullet_Handler1.GetComponent<Rigidbody>();
            if(crystalPosition == null)
            {
                rb.AddForce(transform.forward*500,ForceMode.Impulse);
            }
        }
        shootAudio.Play();
    }

    // Events that happen when the player wins the game
    void VictoryLogic()
    {
        if(DamageScript.destroyed >= 25 && !wasPlayed)
        {
            isGameVictory = true; // Global boolean through which many actions are controlled
            audioScript.CancelInvoke(); 
            victorySound.Play();  // Jesse Audio that get activated when the player wins the game
            victoryPanel.gameObject.SetActive(true); // Victory panel activation with the victory message
            wasPlayed = true;
            characterSkinController.ChangeAnimatorIdle("victory"); // Changing character animation to victory 
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; // Making cursor visible so that the player can choose to restart or end the game
        }
    }

    void Miscellaneous()
    {
        if(inGunMode && ShutDownScript.isShutDown) // Logic when Jesse is in powermode and the battery is over
        {
            Hostler();
        }
        if(batteryBar.timeChange<0.25f) // When Jesse's Battery is less than 25%, Low Battery message pops up
        {
            Debug.Log("Warning : Low Battery!");
            lowBatteryPanel.gameObject.SetActive(true);
        }
        if(batteryBar.timeChange<0.25f && !playedMorePower && inGunMode && !ShutDownScript.isShutDown) // Jesse Informing player to use normal mode as battery is less
        {
            morePower.Play();
            playedMorePower = true;
        }
        if(batteryBar.timeChange>0.25f || ShutDownScript.isShutDown) // If Jesse grabs a power source just after being battery dead 
        {
            lowBatteryPanel.gameObject.SetActive(false);
            playedMorePower = false;
        }
    }

    

    public void OnCollisionEnter(Collision col)
    {
        // Getting the crystal gameobject found and playing jesse audio while keeping track whether it is gunmode or not as well as increasing the count
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
        // Logic to collect the battery powerup to refill the battery, 1. BigBattery - Full Health Regen, 2. SmallBattery - Half Health Regen
        if(other.gameObject.CompareTag("BigBattery"))
        {
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
}
