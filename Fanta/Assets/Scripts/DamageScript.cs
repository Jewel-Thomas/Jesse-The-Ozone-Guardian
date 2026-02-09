using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageScript : MonoBehaviour
{
    public float health = 200;
    public static int destroyed = 0;
    public TextMeshProUGUI destroyedText;
    // public bool isDestroyed = false;
    public AudioSource shatterSound;
    public GameObject fractured;
    public GameObject fireEffect;
    bool isFaded = false;
    public ParticleSystem ps;
    public static float emissionRate = 25.0f;
    


    // Start is called before the first frame update
    void Start()
    {
        emissionRate = 100;
        destroyed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.Alpha9))
        {
            destroyed = 25;
        }
        #endif
        // Events that happen once the crystal is fractured
        if(health<=0)
        {
            Vector3 oldPos = transform.position;
            GameObject FractureClone = Instantiate(fractured,oldPos,Quaternion.identity) as GameObject; // Replacing the fractured gameobject
            GameObject fire = Instantiate(fireEffect,new Vector3(oldPos.x,oldPos.y+1.5f,oldPos.z),Quaternion.identity) as GameObject;
            Destroy(gameObject); // Destroying the current gameobject
            Destroy(FractureClone,12); // Destroy the fractured pieces after a certain amount of time to favour performance
            Destroy(fire,20);
            shatterSound.Play(); // Sound of Crystal Shattering
            destroyed++;
            //isDestroyed = true;
            destroyedText.text = "Crystals Destroyed : " + destroyed; // Displaying the score
            // Changing the emmisionrate of the particle cloud 
            emissionRate-=4;
            Mathf.Clamp(emissionRate,0,1000);
            var temp = ps.emission;
            temp.rateOverTime = emissionRate;
        }

    }

}
