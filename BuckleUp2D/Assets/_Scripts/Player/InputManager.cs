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
        //normalized move direction
        MoveDirection = new Vector3(Input.GetAxis("HorizontalMove"), 0, MoveDirection.z = Input.GetAxis("VerticalMove"));
        MoveDirection = Vector3.Normalize(MoveDirection);

        //normalized aim direction
        AimDirection = new Vector3(Input.GetAxis("HorizontalAim"), 0, Input.GetAxis("VerticalAim"));
        AimDirection = Vector3.Normalize(AimDirection);

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
