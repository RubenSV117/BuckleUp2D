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
    [SerializeField] private float xSensitivity = 4;
    [SerializeField] private float ySensitivity = 3;
    [SerializeField] private bool invertHorizontalAim;
    [SerializeField] private bool invertVerticalAim;
    [SerializeField] private float maxVerticle = 10;
    [SerializeField] private float minVerticle = -35;

    private void Update()
    {
        playerAnim.OverTheShoulderAim();
    }

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
        player.Rotate(Vector3.up, Input.GetAxis("HorizontalAim") * Time.deltaTime * xSensitivity);
        player.Rotate(Vector3.right, Input.GetAxis("VerticalAim") * Time.deltaTime * -ySensitivity);

        player.eulerAngles = new Vector3(player.eulerAngles.x, player.eulerAngles.y, 0);
    }

    public void ResetAim()
    {
        player.eulerAngles = Vector3.zero;
    }
}
