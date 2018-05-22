using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Manages player movement while aiming, switching to 3d position shooter controls 
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class PlayerAimMovement : PlayerMovement
{
    [SerializeField] private float sensitivity = 2;
    [SerializeField] private bool invertHorizontalAim;
    [SerializeField] private bool invertVerticalAim;

    public override void Move(Vector3 direction)
     {
         Vector3 moveDirection = Vector3.zero;

         moveDirection += Input.GetAxis("VerticalMove") * player.transform.forward;
         moveDirection += Input.GetAxis("HorizontalMove") * player.transform.right;

         moveDirection = Vector3.Normalize(moveDirection);

         rigidB.velocity = moveDirection * walkSpeed;
     }

     public override void Turn(Vector3 direction)
     {
         player.Rotate(Vector3.up, Input.GetAxis("HorizontalAim") * (invertHorizontalAim ? -sensitivity : sensitivity));
         player.Rotate(Vector3.right, Input.GetAxis("VerticalAim") * (invertVerticalAim ? sensitivity : -sensitivity));
     }

    public void ResetAim()
    {
        player.eulerAngles = Vector3.zero;
    }
}
