using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using starterAssets;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    private startAssetInput startAssetInput;
    //private ThirdPersonController thirdPersonController;
    private void Awake()
    {
        startAssetInput = GetComponent<startAssetInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(startAssetInput.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
        }
    }
}
