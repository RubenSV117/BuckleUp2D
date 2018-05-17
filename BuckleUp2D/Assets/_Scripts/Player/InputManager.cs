using UnityEngine;

/// <summary>
/// Manages controller input
/// Sends info to PlayerAnimation
/// 
/// Ruben Sanchez
/// 5/16/18
/// </summary>

public class InputManager : MonoBehaviour
{
    private Rigidbody2D rigidB;
    private PlayerMovement playerMove;
    private WeaponManager weapon;

    private Vector2 currentDirection;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<WeaponManager>();
    }

    void Update ()
    {
        //movement

        Vector2 direction = Vector2.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        direction = Vector3.Normalize(direction);
        currentDirection = direction;

        if (!Input.GetButton("Jump"))                   
            playerMove.Move(direction);
           
        //roll

        if (Input.GetButtonDown("Jump") && direction.magnitude > 0)
        {
            playerMove.Roll(direction);
        }

        // fire
        if (Input.GetAxis("Fire1") != 0)
        {
            weapon.Attack(currentDirection);
        }
       
            

    }
}
