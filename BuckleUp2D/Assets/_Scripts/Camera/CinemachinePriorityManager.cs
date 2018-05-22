using Cinemachine;
using UnityEngine;

/// <summary>
/// Manages cinemachine priorities for blending
/// 
/// Ruben Sanchez
/// 5/21/18
/// </summary>

public class CinemachinePriorityManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera idleCam;
    [SerializeField] private CinemachineVirtualCamera runCam;
    [SerializeField] private CinemachineVirtualCamera sprintCam;


    public void Sprint()
    {
        ResetCams();
        sprintCam.Priority = 1;
    }

    public void Run()
    {
        ResetCams();
        runCam.Priority = 1;
    }

    public void Idle()
    {
        ResetCams();
        idleCam.Priority = 1;
    }

    public void ResetCams()
    {
        idleCam.Priority = 0;
        runCam.Priority = 0;
        sprintCam.Priority = 0;
    }
}
