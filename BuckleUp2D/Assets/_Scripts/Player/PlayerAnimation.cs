using System;
using UnityEngine;

/// <summary>
/// Manages player animations
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rigidB;

    private float verticalFlipThreshold = .8f;

    private string rollRightAnimation = "Roll_Right";
    private string rollLeftAnimation = "Roll_Left";
    private string rollUpAnimation = "Roll_Up";
    private string rollDownAnimation = "Roll_Down";
    private string moveAnimation = "Move"; // temp bobbing anim for movement

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rigidB = GetComponentInParent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 movement = new Vector3(rigidB.velocity.x, 0, rigidB.velocity.z);
        anim.SetFloat("Speed", movement.magnitude);
    }

    public void Aim(Vector3 moveDirection, Vector3 aimDirection)
    {
        //not aiming
        if (aimDirection.magnitude == 0)
        {
            anim.SetBool("Aiming", false);
        }

        else
        {
            anim.SetBool("Aiming", true);

            // aiming without moving
            if (Input.GetAxis("HorizontalMove") == 0 && Input.GetAxis("VerticalMove") == 0)
            {
                anim.SetFloat("AimX", 0);
                anim.SetFloat("AimZ", 0);
            }

            // moving and aiming
            else
            {
                // aiming in the direction of movement
                if (Vector3.Dot(moveDirection, aimDirection) >= .5f)               
                    SetAnimAim(0, 1);

                // aiming in opposite direction of movement
                else if (Vector3.Dot(moveDirection, aimDirection) <= -.5f)                
                    SetAnimAim(0, -1);
               
                // aiming in to the right of movement
                else if (Vector3.Dot(transform.right, moveDirection) >= .5f)
                    SetAnimAim(1, 0);

                // aiming in to the right of movement
                else if (Vector3.Dot(transform.right, moveDirection) <= -.5f)
                    SetAnimAim(-1, 0);
            }         
        }
    }

    public void SetAnimAim(float x, float z)
    {
        anim.SetFloat("AimX", x);
        anim.SetFloat("AimZ", z);
    }

    public void SetSpeed(float speed)
    {
        anim.speed = speed;
    }
}
