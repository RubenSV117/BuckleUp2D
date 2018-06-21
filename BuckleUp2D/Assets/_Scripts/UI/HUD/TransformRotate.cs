using UnityEngine;

/// <summary>
/// Rotates a transform around a parent according to input direction
/// 
/// Ruben Sanchez
/// 6/21/18
/// </summary>

public class TransformRotate : MonoBehaviour 
{
    [SerializeField] private float offsetDistance;
    [SerializeField] private Transform moveMidPoint;
    [SerializeField] private Transform aimMidPoint;
    [SerializeField] private float damping;

    private InputManager input;

    // current position of the movable centers of the sticks
    private Vector2 currentMovePoint; 
    private Vector2 currentAimPoint;

    private void Awake()
    {
        input = GameManager.Instance.Input;
    }

    void Update () 
	{
		if(input.MoveDirection.magnitude != 0)
        {
            currentMovePoint = Vector2.Lerp(currentMovePoint, (Vector2)transform.position + offsetDistance * input.MoveDirection, Time.deltaTime / damping);
            moveMidPoint.position = currentMovePoint;
        }

        if (input.AimDirection.magnitude != 0)
        {
            currentAimPoint = Vector2.Lerp(currentAimPoint, (Vector2)transform.position + offsetDistance * input.AimDirection, Time.deltaTime / damping);
            aimMidPoint.position = currentAimPoint;
        }
    }
}
