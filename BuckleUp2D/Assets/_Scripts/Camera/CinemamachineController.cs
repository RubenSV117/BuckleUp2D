﻿using Cinemachine;
using UnityEngine;

/// <summary>
/// Enables Cinemachine cams
/// Subscribes methods to InputManager's events
/// 
/// Ruben Sanchez
/// 5/28/18
/// </summary>


public class CinemamachineController : MonoBehaviour
{
    [SerializeField] private GameObject[] vCams;
    [SerializeField] private GameObject defaultCam;
    [SerializeField] private GameObject aimCam;
    [SerializeField] private GameObject sprintCam;

    [SerializeField] private InputManager input;

    private void Awake()
    {
        input.OnAimBegin += ActivateAimCam;
        input.OnAimEnd += ActivateDefaultCam;
        input.OnSprintChange += ActivateSprintCam;
    }

    void Start () 
	{
	    ActivateCam(defaultCam);
	}

    public void ActivateCam(GameObject targetCam)
    {
        targetCam.SetActive(true);

        foreach (var cam in vCams)
        {
            if (cam != targetCam)
                cam.SetActive(false);
        }
    }

    public void ActivateDefaultCam()
    {
        ActivateCam(defaultCam);
    }

    public void ActivateAimCam()
    {
        ActivateCam(aimCam);
    }

    public void ActivateSprintCam(bool isSprinting)
    {
        ActivateCam(isSprinting ? sprintCam : defaultCam);
    }
}
