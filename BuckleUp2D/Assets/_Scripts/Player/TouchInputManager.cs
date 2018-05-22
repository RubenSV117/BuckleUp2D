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
    [SerializeField]
    private UnityEvent onMoveTouch;

    [SerializeField]
    private UnityEvent onMoveRelease;

    [SerializeField]
    private UnityEvent onShootTouch;

    [SerializeField]
    private UnityEvent onShootRelease;

    private Vector2 initialMoveTouchPosition;
    private Vector2 initialShootTouchPosition;
    private float minDeltaThreshold = .75f;

    private PlayerMovement _playerMove;
    private WeaponManager weapon;
    private DirectionalArrows directionalArrows;


    private void Awake()
    {
        _playerMove = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<WeaponManager>();
        directionalArrows = GetComponent<DirectionalArrows>();
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

            // after move touch dragged past delta threshold
	        if ((touch.phase == TouchPhase.Stationary  || touch.phase == TouchPhase.Moved) && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	        {
	            Vector2 delta = touch.position - initialMoveTouchPosition;

	            if (delta.magnitude > minDeltaThreshold)
	            {
	                Vector2 direction = Vector3.Normalize(delta);

                    _playerMove.Move(direction);
	                directionalArrows.UpdateMoveArrow(delta);
                    onMoveTouch.Invoke();
                }
	        }

	        // release of move touch
	        else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x < .5f)
	        {
	            Vector2 delta = touch.position - initialMoveTouchPosition;

	            if (delta.magnitude > minDeltaThreshold)
	            {
	                Vector2 direction = Vector3.Normalize(delta);
	                _playerMove.Move(direction);
	            }

	            onMoveRelease.Invoke();
	        }

            // initial right side touch = shoot touch
            else if (touch.phase == TouchPhase.Began && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	        {
	            initialShootTouchPosition = touch.position;
	        }

	        // shoot touch changed position
	        if (touch.phase == TouchPhase.Moved && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
	        {
	            Vector2 delta = touch.position - initialShootTouchPosition;

	            if (delta.magnitude > minDeltaThreshold && !directionalArrows.shootArrow.activeInHierarchy)
	                onShootTouch.Invoke();

	            directionalArrows.UpdateShootArrow(delta);
	        }

            // release of shoot touch
            else if (touch.phase == TouchPhase.Ended && Camera.main.ScreenToViewportPoint(touch.position).x > .5f)
            {
                Vector2 delta = touch.position - initialShootTouchPosition;

                if (delta.magnitude > minDeltaThreshold)
                {
                    Vector2 direction = Vector3.Normalize(delta);
                    weapon.Attack(direction);
                }

                onShootRelease.Invoke();
            }
        }

	    if (Input.touchCount == 0)
	    {
	        directionalArrows.shootArrow.SetActive(false);
	        directionalArrows.moveArrow.SetActive(false);
        }
    }
}
