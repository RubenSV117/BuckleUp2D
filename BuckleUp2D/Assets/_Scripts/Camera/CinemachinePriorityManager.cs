using Cinemachine;
using UnityEngine;

/// <summary>
/// Manages cinemachine vcam for blending
/// 
/// Ruben Sanchez
/// 5/21/18
/// </summary>

public class CinemachinePriorityManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera idleCam;
    [SerializeField] private CinemachineVirtualCamera runCam;
    [SerializeField] private CinemachineVirtualCamera sprintCam;
    [SerializeField] private CinemachineVirtualCamera aimCam;


    public void Sprint()
    {
        ResetCams();
        sprintCam.gameObject.SetActive(true);
    }

    public void Run()
    {
        ResetCams();
        runCam.gameObject.SetActive(true);
    }

    public void Idle()
    {
        ResetCams();
        idleCam.gameObject.SetActive(true);
    }

    public void Aim()
    {
        ResetCams();
        aimCam.gameObject.SetActive(true);
    }

    public void ResetCams()
    {
        idleCam.gameObject.SetActive(false);
        runCam.gameObject.SetActive(false);
        sprintCam.gameObject.SetActive(false);
    }
}
