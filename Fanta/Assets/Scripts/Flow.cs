using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow : MonoBehaviour
{
    [Range(0,5)] public float scrollspeed;
    Renderer rend;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = scrollspeed*Time.time;
        rend.material.mainTextureOffset = new Vector2(offset,offset/2);
    }
}
