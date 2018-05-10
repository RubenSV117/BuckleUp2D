using System;
using UnityEngine;

/// <summary>
/// Controls touch input for the player
/// 
/// Ruben Sanchez
/// 4/26/19
/// </summary>

public class TouchControlManager : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject projectile;

    private Touch moveTouch;
    private int moveTouchIndex = -1;
    private Vector2 initialMoveTouchPosition;

    private int shootTouchIndex = -1;
    private Vector2 initialShootTouchPosition;

    private Rigidbody2D rigidB;

    


    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
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
                rigidB.velocity = direction * moveSpeed;
            }

            // release of shoot touch
            else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
            {
                Vector2 direction = Vector3.Normalize(touch.position - initialShootTouchPosition);

                Projectile proj = Instantiate(projectile, transform.position, projectile.transform.rotation).GetComponent<Projectile>();
                proj.Shoot(direction);
            }
        }
    }
}
