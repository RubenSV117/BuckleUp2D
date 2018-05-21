using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages controller input
/// Sends directions to PlayerMovement, WeaponManager, CharacterFlip
/// Uses SlowdownManager for slow-mo
/// 
/// 
/// Ruben Sanchez
/// 5/16/18
/// </summary>

public class InputManager : MonoBehaviour
{
    private PlayerMovement playerMove;
    private WeaponManager weapon;
    private SlowDownManager sloMo;
    private PlayerAnimation playerAnim;

    private Vector3 currentDirection;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<WeaponManager>();
        sloMo = GetComponent<SlowDownManager>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    void Update ()
    {
  
#region Move/Fire Directions

        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = Input.GetAxis("HorizontalMove");
        moveDirection.z = Input.GetAxis("VerticalMove");
        moveDirection = Vector3.Normalize(moveDirection);

        Vector3 aimDirection = Vector3.zero;
        aimDirection.x = Input.GetAxis("HorizontalAttack");
        aimDirection.z = Input.GetAxis("VerticalAttack");
        aimDirection = Vector3.Normalize(aimDirection);

        #endregion

        //update current direction, fire direction overrides movement for character flip
        playerAnim.Aim(moveDirection, aimDirection);

        if (aimDirection.magnitude != 0)
        {
            playerMove.Turn(aimDirection);
            currentDirection = aimDirection;
        }
            
        else if (moveDirection.magnitude != 0)
        {
            playerMove.Turn(moveDirection);
            currentDirection = moveDirection;
        }
       
        //move
        playerMove.Move(moveDirection);

        //roll
        if (Input.GetButtonDown("Roll"))
        {
            if(moveDirection.magnitude != 0)
                playerMove.Roll(moveDirection);

            else
                playerMove.Roll(currentDirection);
        }
        
        //fire
        if (Input.GetAxis("Fire") != 0)
        {
            if (aimDirection.magnitude != 0)
            {
                weapon.Attack(aimDirection);

                if(moveDirection.magnitude != 0)
                    playerAnim.Aim(moveDirection, aimDirection);
            }

            else
            {
                weapon.Attack(currentDirection);
                if (moveDirection.magnitude != 0)
                    playerAnim.Aim(moveDirection, moveDirection);
            }
               
        }
           
        //slowMo
        if (Input.GetButtonDown("SlowMo"))
            sloMo.SlowDown();

        //sprint
        playerMove.SetSprint(Input.GetButton("Sprint"));
    }
}
