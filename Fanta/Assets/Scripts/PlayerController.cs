using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioSource chimingAudio;
    public bool isChiming = false;
    public GameObject crystalPrefab;
    public Material crystalMaterial;
    public  bool isGlowing = false;
    // Start is called before the first frame update
    void Start()
    {
        crystalPrefab = this.gameObject;
        crystalMaterial = crystalPrefab.GetComponent<MeshRenderer>().material;
        crystalMaterial.DisableKeyword("_EMISSION");   
    }

   

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            StartCoroutine(EagleVision());
        }
    }

    IEnumerator EagleVision()
    {
        crystalMaterial.EnableKeyword("_EMISSION");
        yield return new WaitForSeconds(10);
        crystalMaterial.DisableKeyword("_EMISSION");
    }

    // if(isChiming)
    // {
    //     chimingAudio.Play();
    // }
    // public void OnCollisionEnter(Collision col)
    // {
    //     if(col.gameObject.CompareTag("Crystal"))
    //     {
    //         Destroy(col.gameObject);
    //     }
    // }
}
