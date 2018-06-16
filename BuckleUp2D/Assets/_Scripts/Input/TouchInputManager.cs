using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Detects touch input for the player
/// Sends direction to PlayerMovement
/// Sends direction to Weapon
/// Sends direction and deltaMovement to DirectionArrows
/// 
/// Ruben Sanchez
/// 4/26/19
/// </summary>

public class TouchInputManager : MonoBehaviour
{
    [HideInInspector] public Vector3 MoveDirection;
    [HideInInspector] public Vector3 AimDirection;

    public delegate void Attack();
    public event Attack OnAttack;
    public event Attack OnAttackEnd;

    public delegate void Roll();
    public event Roll OnRoll;

    public delegate void Interact();
    public event Interact OnInteract;

    public delegate void WeaponCycle();
    public event WeaponCycle OnWeaponCycle;

    public delegate void Sprint(bool isSprinting);
    public event Sprint OnSprintChange;

    public delegate void Aim(bool isAiming);
    public event Aim OnAimChange;




    private PlayerMovement playerMove;
    private WeaponManager weaponManager;

    public bool IsAiming { get; internal set; }

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        weaponManager = GetComponent<WeaponManager>();
    }

    void Update () 
	{
        
    }
}
