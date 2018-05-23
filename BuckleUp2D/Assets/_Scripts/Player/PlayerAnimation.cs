using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages player animations
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private UnityEvent onAim;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void FreeRunAim(Vector3 moveDirection, Vector3 aimDirection)
    {
        //not aiming
        if (aimDirection.magnitude == 0)
        {
            anim.SetBool("FreeRunAiming", false);
        }

        else
        {
            anim.SetBool("FreeRunAiming", true);

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
                if (Vector3.Dot(moveDirection, aimDirection) >= .2f)               
                    SetAnimAim(0, 1);

                // aiming in opposite direction of movement
                else if (Vector3.Dot(moveDirection, aimDirection) <= -.2f)                
                    SetAnimAim(0, -1);
               
                // aiming in to the right of movement
                else if (Vector3.Dot(transform.right, moveDirection) >= .2f)
                    SetAnimAim(1, 0);

                // aiming in to the right of movement
                else if (Vector3.Dot(transform.right, moveDirection) <= -.2f)
                    SetAnimAim(-1, 0);
            }         
        }
    }

    public void OverTheShoulderAim()
    {
        if (Input.GetAxis("HorizontalMove") == 0 && Input.GetAxis("VerticalMove") == 0)
            SetAnimAim(0, 0);

        // forward Movement
        else if (Input.GetAxis("VerticalMove") > 0)
            SetAnimAim(0, 1);

        // back movement
        else if (Input.GetAxis("VerticalMove") < 0)
            SetAnimAim(0, -1);

        // right movement
        else if (Input.GetAxis("HorizontalMove") > 0)
            SetAnimAim(1, 0);
        
        //left movment    
        else if (Input.GetAxis("HorizontalMove") < 0)
            SetAnimAim(-1, 0);

     
    }


    public void SetAnimAim(float x, float z)
    {
        anim.SetFloat("AimX", x);
        anim.SetFloat("AimZ", z);
    }

    public void OverTheShoulderAim(bool otsAiming)
    {
        anim.SetBool("OverTheShoulderAiming", otsAiming);

        if(otsAiming)
            onAim.Invoke();
    }


    public void SetSpeed(float speed)
    {
        anim.SetFloat("Speed", speed);
    }

    public void SetSprint(bool sprinting)
    {
        anim.SetBool("Sprinting", sprinting);
    }
}
