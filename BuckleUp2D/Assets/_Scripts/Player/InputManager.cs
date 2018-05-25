using UnityEngine;
using UnityEngine.Events;

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

    public delegate void Aim();
    public event Aim OnAim;

    public delegate void WeaponCycle();
    public event WeaponCycle OnWeaponCycle;

    public delegate void SprintStateChange(bool isSprinting);
    public event SprintStateChange OnSprintChange;

    void Update()
    { 
        MoveDirection = new Vector3(Input.GetAxis("HorizontalMove"), 0, Input.GetAxis("VerticalMove"));      
        AimDirection = new Vector3(Input.GetAxis("HorizontalAim"), 0, Input.GetAxis("VerticalAim"));

        //attack
        if (Input.GetAxis("Attack") != 0)
            OnAttack.Invoke();

        //aim
        if(Input.GetAxis("Aim") != 0 && OnAim != null)
            OnAim.Invoke();

        //roll
        if (Input.GetButtonDown("Roll") && OnRoll != null)
            OnRoll.Invoke();

        //slowMo
        if (Input.GetButtonDown("SlowMo") && OnSlowMo != null)
            OnSlowMo.Invoke();

        //cycle weapon
        if (Input.GetButtonDown("CycleWeapon") && OnWeaponCycle != null)
            OnWeaponCycle.Invoke();

        //sprint state change
        if (Input.GetButtonDown("Sprint") && OnSprintChange != null)
            OnSprintChange.Invoke(true);

        if (Input.GetButtonUp("Sprint") && OnSprintChange != null)
            OnSprintChange.Invoke(false);
    }
}
