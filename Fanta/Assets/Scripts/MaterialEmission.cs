using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialEmission : MonoBehaviour
{
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            material.EnableKeyword("_EMISSION");
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            material.DisableKeyword("_EMISSION");
        }
    }
}
