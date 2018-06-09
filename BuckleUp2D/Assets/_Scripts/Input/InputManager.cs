using UnityEditor;
using UnityEngine;

/// <summary>
/// Manages controller Input
/// 
/// Ruben Sanchez
/// 5/16/18
/// </summary>

public class InputManager : MonoBehaviour
{
    public Vector2 AimSensitivity = new Vector2(6, .6f);
    public Vector2 AimDamping = new Vector2(.05f, .1f);

    public bool IsAiming;

    [HideInInspector] public Vector3 MoveDirection;
    [HideInInspector] public Vector3 AimDirection;

    public delegate void Attack();
    public event Attack OnAttack;
    public event Attack OnAttackEnd;

    public delegate void Roll();
    public event Roll OnRoll;

    public delegate void SlowMo();
    public event SlowMo OnSlowMo;

    public delegate void Interact();
    public event Interact OnInteract;

    public delegate void Aim(bool isAiming);
    public event Aim OnAimChange;

    public delegate void WeaponCycle();
    public event WeaponCycle OnWeaponCycle;

    public delegate void Sprint(bool isSprinting);
    public event Sprint OnSprintChange;

    [Tooltip("Amount of normal sensitivity to use when zoomed aiming")]
    [SerializeField] private float aimSensitivityMultiplier = .2f;

    private bool isAttacking;
 
    void Update()
    {
        MoveDirection = new Vector3(Input.GetAxis("HorizontalMove"), 0, Input.GetAxis("VerticalMove"));
        AimDirection = new Vector3(Input.GetAxis("HorizontalAim"), 0, Input.GetAxis("VerticalAim"));

        //attack
        if (Input.GetAxis("Attack") != 0 && OnAttack != null)
        {
            if (!isAttacking)
                isAttacking = true;

            OnAttack.Invoke();
            OnSprintChange.Invoke(false); // cancel sprint on attack
        }

        // attack end
        if (Input.GetAxis("Attack") == 0 && OnAttackEnd != null && isAttacking)
        {
            isAttacking = false;

            OnAttackEnd.Invoke();
        }


        //aim begin
        if (Input.GetAxis("Aim") != 0 && OnAimChange != null && !IsAiming)
        {
            IsAiming = true;
            OnAimChange.Invoke(true);
            AimSensitivity *= aimSensitivityMultiplier;
        }

        //aim end
        if (Input.GetAxis("Aim") == 0 && OnAimChange != null && IsAiming)
        {
            IsAiming = false;
            OnAimChange.Invoke(false);
            AimSensitivity /= aimSensitivityMultiplier;
        }

        //roll
        if (Input.GetButtonDown("Roll") && OnRoll != null)
            OnRoll.Invoke();

        //slowMo
        if (Input.GetButtonDown("SlowMo") && OnSlowMo != null)
            OnSlowMo.Invoke();

        //cycle weapon
        if (Input.GetButtonDown("CycleWeapon") && OnWeaponCycle != null)
            OnWeaponCycle.Invoke();

        //sprint begin
        if (Input.GetButtonDown("Sprint") && OnSprintChange != null)
            OnSprintChange.Invoke(true);
        
        //sprint end
        if (Input.GetButtonUp("Sprint") && OnSprintChange != null)
            OnSprintChange.Invoke(false);

        //interact
        if (Input.GetButtonDown("Interact") && OnInteract != null)
            OnInteract.Invoke();
    }
}
