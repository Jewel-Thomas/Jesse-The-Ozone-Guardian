using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Script : MonoBehaviour
{
    public Transform tiger;
    public float speedH = 0.0f;
    public float speedV = 0.0f;
    public float yaw = 0.0f;
    public float pitch = 0.0f;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - tiger.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPosition = tiger.transform.position + offset;
        yaw += speedH*Input.GetAxis("Mouse X");
        pitch -= speedV*Input.GetAxis("Mouse Y");
        transform.eulerAngles = new Vector3(pitch,yaw,0.0f);
        transform.LookAt(tiger.transform);
        transform.position = newPosition;
    }
}
