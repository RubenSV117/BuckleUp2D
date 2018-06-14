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


    private Vector2 initialMoveTouchPosition;
    private Vector2 initialShootTouchPosition;
    private float minDeltaThreshold = .75f;

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
        // Manage current touches
	    foreach (Touch touch in Input.touches)
	    {            
	        // initial left side touch for movement
            if (touch.phase == TouchPhase.Began && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	        {
	            initialMoveTouchPosition = touch.position;
            }

            // movement touch dragged passed threshold
	        if ((touch.phase == TouchPhase.Stationary  || touch.phase == TouchPhase.Moved) && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	        {
	            Vector2 delta = touch.position - initialMoveTouchPosition;

	            MoveDirection = delta.magnitude > minDeltaThreshold ? Vector3.Normalize(new Vector3(delta.x, 0, delta.y)) : Vector3.zero;
	        }

	        // release of move touch
	        else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	            MoveDirection = Vector3.zero;


            // initial right side touch for aiming
            else if (touch.phase == TouchPhase.Began && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	        {
	            initialShootTouchPosition = touch.position;
	        }

	        // aim touch dragged passed threshold
	        if (touch.phase == TouchPhase.Moved && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	        {
	            Vector2 delta = touch.position - initialShootTouchPosition;

	            AimDirection = delta.magnitude > minDeltaThreshold ? Vector3.Normalize(new Vector3(delta.x, 0, delta.y)) : Vector3.zero;

	            OnAttack.Invoke();

	        }

	        // release of aim touch
	        else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	            AimDirection = Vector3.zero;
        }
    }
}
