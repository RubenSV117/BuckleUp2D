using UnityEngine;

/// <summary>
/// Rotates a transfrom according to input, used to rotate the look at target for cinemachine
/// 
/// Ruben Sanchez
/// 6/17/18
/// </summary>

public class LookAtRotator : MonoBehaviour 
{
    private InputManager input;

    private void Awake()
    {
        input = GameManager.Instance.Input;
    }

	void Update ()
	{
        if(input.AimDirection.magnitude != 0)
            transform.right = input.AimDirection;

        else if (input.MoveDirection.magnitude != 0)
            transform.right = input.MoveDirection;
    }
}
