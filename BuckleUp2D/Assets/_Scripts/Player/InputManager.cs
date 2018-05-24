using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages controller input
/// Sends directions to PlayerMovement, WeaponManager
/// Uses SlowdownManager for slow-mo
/// 
/// 
/// Ruben Sanchez
/// 5/16/18
/// </summary>

public class InputManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement freeRunMovement;
    [SerializeField] private PlayerMovement aimMovement;

    private WeaponManager weapon;
    private SlowDownManager sloMo;
    private PlayerAnimation playerAnim;
    private WeaponManager weaponManager;

    private Vector3 currentDirection;
    private PlayerMovement currentMovement;


    private void Awake()
    {
        freeRunMovement = GetComponent<PlayerMovement>();
        currentMovement = freeRunMovement;
        weapon = GetComponentInChildren<WeaponManager>();
        playerAnim = GetComponent<PlayerAnimation>();
        weaponManager = GetComponentInChildren<WeaponManager>();
    }

    private void Start()
    {
        sloMo = GameManager.Instance.SlowManager;
    }

    void Update()
    {

        #region Move/Fire Directions

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.GetAxis("HorizontalMove");
        moveDirection.z = Input.GetAxis("VerticalMove");

        Vector3 aimDirection = Vector3.zero;
        aimDirection.x = Input.GetAxis("HorizontalAim");
        aimDirection.z = Input.GetAxis("VerticalAim");
        aimDirection = Vector3.Normalize(aimDirection);

        #endregion

        //update current direction, fire direction overrides movement for character flip
        playerAnim.FreeRunAim(moveDirection, aimDirection);

        if (aimDirection.magnitude != 0)
        {
            currentMovement.Turn(aimDirection);
            currentDirection = aimDirection;
        }

        else if (moveDirection.magnitude != 0)
        {
            currentMovement.Turn(moveDirection);
            currentDirection = moveDirection;
        }

        //move
        currentMovement.Move(moveDirection);

        //roll
        if (Input.GetButtonDown("Roll"))
        {
            if (moveDirection.magnitude != 0)
                freeRunMovement.Roll(moveDirection);

            else
                freeRunMovement.Roll(currentDirection);
        }

        //fire
        if (Input.GetAxis("Fire") != 0)
        {
            if (aimDirection.magnitude != 0)
            {
                weapon.Attack(aimDirection);

                if (moveDirection.magnitude != 0)
                    playerAnim.FreeRunAim(moveDirection, aimDirection);
            }

            else
            {
                weapon.Attack(currentDirection);
                if (moveDirection.magnitude != 0)
                    playerAnim.FreeRunAim(moveDirection, moveDirection);
            }

        }

        //slowMo
        if (Input.GetButtonDown("SlowMo"))
            sloMo.SlowDown();

        //cycle weapon
        if (Input.GetButtonDown("CycleWeapon"))
            weaponManager.CycleWeapons();


        //sprint
        if (Input.GetButtonDown("Sprint"))
            freeRunMovement.SetSprint(true);

        if (Input.GetButtonUp("Sprint"))
            freeRunMovement.SetSprint(false);

        //aim
        currentMovement = Input.GetAxis("OTSAim") != 0 ? aimMovement : freeRunMovement;

        playerAnim.OverTheShoulderAim(Input.GetAxis("OTSAim") != 0);
    }
}
