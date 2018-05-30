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
    public Transform targetTransform;

    [SerializeField] private InputManager input;
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    private float verticalValue;

    public void VerticalTurn()
    {
       
        targetTransform.localPosition += targetTransform.up * input.AimDirection.z * input.AimSensitivity.y;

        if(targetTransform.localPosition.y > maxHeight)
            targetTransform.localPosition = new Vector3(targetTransform.localPosition.x, maxHeight, targetTransform.localPosition.z);

        else if (targetTransform.localPosition.y < minHeight)
            targetTransform.localPosition = new Vector3(targetTransform.localPosition.x, minHeight, targetTransform.localPosition.z);
    }
}
