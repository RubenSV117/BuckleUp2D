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

    [SerializeField] private InputManager input;

    private void Awake()
    {
        input.OnAimBegin += ActivateAimBeginCam;
        input.OnAimEnd += ActivateDefaultCam;
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

    public void ActivateAimBeginCam()
    {
        ActivateCam(aimCam);
    }
}
