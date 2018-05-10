using UnityEngine;

/// <summary>
/// Detects touch input for the player
/// Sends direction to PlayerMovement
/// Sends direction to Weapon
/// 
/// Ruben Sanchez
/// 4/26/19
/// </summary>

public class TouchInputManager : MonoBehaviour
{
    private Vector2 initialMoveTouchPosition;
    private Vector2 initialShootTouchPosition;

    private PlayerMovement playerMove;
    private Weapon weapon;

    private void Awake()
    {
        playerMove = GetComponent<PlayerMovement>();
        weapon = GetComponent<Weapon>();
    }

    void Update () 
	{
	    foreach (Touch touch in Input.touches)
	    {            
	        // initial left side touch = move touch
            if (touch.phase == TouchPhase.Began && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	        {
	            initialMoveTouchPosition = touch.position;
	        }

	        // initial right side touch = shoot touch
            else if (touch.phase == TouchPhase.Began && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	        {
	            initialShootTouchPosition = touch.position;
	        }

            // release of move touch
            else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
            {
                Vector2 direction = Vector3.Normalize(touch.position - initialMoveTouchPosition);
                playerMove.Move(direction);
            }

            // release of shoot touch
            else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
            {
                Vector2 direction = Vector3.Normalize(touch.position - initialShootTouchPosition);
                weapon.Fire(direction);
            }
        }
    }
}
