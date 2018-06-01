using Cinemachine;
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
        // subscribe all camera enabling methods to input manager events
        input.OnAimChange += AimChange;
        input.OnSprintChange += ActivateSprintCam;
    }

    void Start () 
	{
	    ActivateCam(defaultCam);
	}

    
    // Activate target camera, deactive the rest
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

    public void AimChange(bool isAiming)
    {
       ActivateCam(isAiming ? aimCam : defaultCam);
    }

    public void ActivateSprintCam(bool isSprinting)
    {
        ActivateCam(isSprinting ? sprintCam : defaultCam);
    }
}
