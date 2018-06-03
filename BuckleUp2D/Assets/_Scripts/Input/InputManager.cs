using UnityEditor;
using UnityEngine;

/// <summary>
/// Manages controller input
/// 
/// Ruben Sanchez
/// 5/16/18
/// </summary>

public class InputManager : MonoBehaviour
{
    public Vector2 AimSensitivity;
    public Vector2 AimDamping;

    [HideInInspector] public Vector3 MoveDirection;
    [HideInInspector] public Vector3 AimDirection;

    public delegate void Attack();
    public event Attack OnAttack;

    public delegate void Roll();
    public event Roll OnRoll;

    public delegate void SlowMo();
    public event SlowMo OnSlowMo;

    public delegate void Aim(bool isAiming);
    public event Aim OnAimChange;

    public delegate void WeaponCycle();
    public event WeaponCycle OnWeaponCycle;

    public delegate void Sprint(bool isSprinting);
    public event Sprint OnSprintChange;

    [Tooltip("Amount of normal sensitivity to use when zoomed aiming")]
    [SerializeField] private float aimSensitivityMultiplier = .7f;
    private bool isAiming;

    void Update()
    {
        MoveDirection = new Vector3(Input.GetAxis("HorizontalMove"), 0, Input.GetAxis("VerticalMove"));
        AimDirection = new Vector3(Input.GetAxis("HorizontalAim"), 0, Input.GetAxis("VerticalAim"));

        //attack
        if (Input.GetAxis("Attack") != 0 && OnAttack != null)
            OnAttack.Invoke();

        //aim begin
        if (Input.GetAxis("Aim") != 0 && OnAimChange != null && !isAiming)
        {
            isAiming = true;
            OnAimChange.Invoke(true);
            AimSensitivity *= aimSensitivityMultiplier;
        }

        //aim end
        if (Input.GetAxis("Aim") == 0 && OnAimChange != null && isAiming)
        {
            isAiming = false;
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
        
            
    }
}
