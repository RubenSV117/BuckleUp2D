using UnityEngine;

/// <summary>
/// Rotates a transfrom according to input, used to rotate the look at target for cinemachine
/// 
/// Ruben Sanchez
/// 6/17/18
/// </summary>

public class LookAtRotator : MonoBehaviour
{
    [SerializeField] private float damping;

    [Tooltip("Offset from the parent transform")]
    [SerializeField] private float lookAhead = 3f;

    [SerializeField] private Transform transformToMove;

    private InputManager input;
    private Vector3 currentPosition;


    private void Awake()
    {
        input = GameManager.Instance.Input;
    }

    void Update ()
	{

        if (input.AimDirection.magnitude != 0 || input.MoveDirection.magnitude != 0)
	    {
            currentPosition = Vector3.Lerp(currentPosition, transform.position + (input.AimDirection.magnitude != 0 ? input.AimDirection : input.MoveDirection * 1.4f) * lookAhead, Time.deltaTime / damping);
	        transformToMove.position = currentPosition;
        }
    }
}
