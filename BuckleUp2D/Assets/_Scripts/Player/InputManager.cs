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
    private CharacterFlip characterFlip;

    private Vector2 currentDirection;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<WeaponManager>();
        sloMo = GetComponent<SlowDownManager>();
        characterFlip = GetComponentInChildren<CharacterFlip>();
    }

    void Update ()
    {
  
#region Move/Fire Directions

        Vector2 moveDirection = Vector2.zero;
        moveDirection.x = Input.GetAxis("HorizontalMove");
        moveDirection.y = Input.GetAxis("VerticalMove");
        moveDirection = Vector3.Normalize(moveDirection);

        Vector2 fireDirection = Vector2.zero;
        fireDirection.x = Input.GetAxis("HorizontalAttack");
        fireDirection.y = Input.GetAxis("VerticalAttack");
        fireDirection = Vector3.Normalize(fireDirection);

        #endregion

        //update current direction, fire direction overrides movement for character flip
        if (fireDirection.magnitude != 0)
            currentDirection = fireDirection;

        else if (moveDirection.magnitude != 0)
            currentDirection = moveDirection;

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
            if(fireDirection.magnitude != 0)
                weapon.Attack(fireDirection);

            else
                weapon.Attack(currentDirection);
        }
           
        //slowMo
        if (Input.GetButtonDown("SlowMo"))
            sloMo.SlowDown();

        //renderers
        if(fireDirection.magnitude != 0)
            characterFlip.FlipCharacterToDirection(currentDirection);

        else
            characterFlip.FlipCharacterToDirection(moveDirection);
    }
}
