using Cinemachine;
using UnityEngine;

/// <summary>
/// Adjust current Vcam look at height to match aiming input
/// 
/// Ruben Sanchez
/// 5/28/18
/// </summary>


public class VerticalAim : MonoBehaviour
{
    public CinemachineVirtualCamera activeCam;

    [SerializeField] private InputManager input;

    private float verticalTurnValue;

    public void VerticalTurn()
    {
        verticalTurnValue = Mathf.Lerp(verticalTurnValue, input.AimDirection.z, Time.deltaTime * (1 / input.AimDamping.y));

       activeCam.LookAt.Rotate(activeCam.LookAt.right, verticalTurnValue * input.AimSensitivity.y);
    }
}
