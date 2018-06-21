using UnityEngine;

/// <summary>
/// Moves a transfrom around its parent according to input, used to smoothly offset the look at target for cinemachine
/// 
/// Ruben Sanchez
/// 6/17/18
/// </summary>

public class TransformOffset : MonoBehaviour
{
    [SerializeField] private float dampingWhileMoving;
    [SerializeField] private float dampingWhileStationary;


    [Tooltip("Offset from the parent transform")]
    [SerializeField] private float lookAhead = 3f;

    [SerializeField] private Transform transformToMove;

    private InputManager input;
    private Vector2 currentPosition;


    private void Awake()
    {
        input = GameManager.Instance.Input;
    }

    void Update ()
	{
        // Move the look at target if moving or aiming
        if (input.AimDirection.magnitude != 0 || input.MoveDirection.magnitude != 0)
	    {
            // lerp between current and target, prioritize aim direction, set appropriate damping
            currentPosition = Vector3.Lerp(currentPosition, 
                (Vector2)transform.position + (input.AimDirection.magnitude != 0 ? input.AimDirection : input.MoveDirection) * lookAhead, 
                Time.deltaTime / (input.MoveDirection.magnitude != 0 ? dampingWhileMoving : dampingWhileStationary));

	        transformToMove.position = currentPosition;
        }
    }
}
