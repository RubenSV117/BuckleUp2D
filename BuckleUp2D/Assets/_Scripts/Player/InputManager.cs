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

        Vector3 moveDirection = Vector2.zero;
        moveDirection.x = Input.GetAxis("HorizontalMove");
        moveDirection.z = Input.GetAxis("VerticalMove");
        moveDirection = Vector3.Normalize(moveDirection);

        Vector3 AttackDirection = Vector2.zero;
        AttackDirection.x = Input.GetAxis("HorizontalAttack");
        AttackDirection.z = Input.GetAxis("VerticalAttack");
        AttackDirection = Vector3.Normalize(AttackDirection);

        #endregion

        //update current direction, fire direction overrides movement for character flip
        playerAnim.Aim(moveDirection, AttackDirection);

        if (AttackDirection.magnitude != 0)
        {
            playerMove.Turn(AttackDirection);
            currentDirection = AttackDirection;
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
        
        // fire
        if (Input.GetAxis("Fire") != 0)
        {
            if(AttackDirection.magnitude != 0)
                weapon.Attack(AttackDirection);

            else
                weapon.Attack(currentDirection);
        }
           
        //slowMo
        if (Input.GetButtonDown("SlowMo"))
            sloMo.SlowDown();
    }
}
