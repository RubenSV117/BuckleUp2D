using UnityEngine;

/// <summary>
/// Controls player animations
/// Gets Input info from InputManager
/// 
/// Ruben Sanchez
/// 5/28/19
/// </summary>
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMove;


    private InputManager input;
    private WeaponManager weaponManager;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        weaponManager = GetComponent<WeaponManager>();

        input = GameManager.Instance.Input;

        // subscribe event methods to Input events
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
        input.OnAimChange += Aim;
        input.OnWeaponCycle += WeaponCycle;

    }

    public void SetMoving()
    {
        // movement
        anim.SetBool("moving", input.MoveDirection.magnitude != 0);
    }

    public void SetMovementDirection()
    {
        // move direction, do not update if currently rolling
        if (!playerMove.isRolling)
        {
            anim.SetFloat("horizontalMove", input.MoveDirection.x);
            anim.SetFloat("verticalMove", input.MoveDirection.z);
        }
       

        // aim direction
        anim.SetFloat("horizontalAim", input.AimDirection.x);
        anim.SetFloat("verticalAim", input.AimDirection.z);
    }

    private void SetSprint(bool isSprinting)
    {
        anim.SetBool("sprint", isSprinting);
    }

    public void Roll()
    {
        anim.Play("Roll");
    }

    public void WeaponCycle()
    {
        if(weaponManager.weapons.Count > 1)
            anim.Play("WeaponCycle", anim.GetLayerIndex("Both Arms"));
    }

    public void Aim(bool isAiming)
    {
        anim.SetBool("aiming", isAiming);
    }
}
