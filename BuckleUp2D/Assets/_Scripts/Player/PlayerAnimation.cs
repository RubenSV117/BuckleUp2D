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
        anim.SetFloat("Speed", rigidB.velocity.magnitude);
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
                anim.SetFloat("AimX", Input.GetAxis("HorizontalAttack"));
                anim.SetFloat("AimZ", Input.GetAxis("VerticalAttack"));
            }

           

            //// aiming while not moving
            //if (Input.GetAxisRaw("HorizontalAttack") == 0 && Input.GetAxisRaw("VerticalAttack") != 0)
            //{
            //    anim.SetFloat("AimX", 0);
            //    anim.SetFloat("AimZ", 1);
            //}

            //// aiming and movement direction are equal
            //else if (Input.GetAxisRaw("HorizontalAttack") - Input.GetAxisRaw("VerticalAttack") == 0)
            //{
            //    anim.SetFloat("AimX", 0);
            //    anim.SetFloat("AimZ", 1);
            //}
        }
    }
}
