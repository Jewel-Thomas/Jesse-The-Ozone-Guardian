using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MyCameraController : MonoBehaviour
{
    
    public TextMeshProUGUI text;
    public int eggCount = 0;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward*speed*verticalInput*Time.deltaTime);
        transform.Rotate(Vector3.up*speed*10*horizontalInput*Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Egg"))
        {
            Destroy(other.gameObject);
            eggCount++;
            text.text = "Eggs Collected : " + eggCount;
        }
        if(eggCount==5)
        {
            text.text = "Hurray! All eggs were Collected!";
        }
    }
}
