using UnityEngine;

/// <summary>
/// Controls player horizontal movement
/// 
/// Ruben Sanchez
/// 5/27/28
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputManager input;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;

    private Rigidbody rigidB;
    private Transform mTransform;


    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        mTransform = transform;
    }

    public void Move()
    {
        Vector3 moveDirection = input.MoveDirection.z * mTransform.forward;
        moveDirection += input.MoveDirection.x * mTransform.right;

        Vector3 moveVelocity = moveDirection * moveSpeed;
        rigidB.velocity = new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);
    }

    public void Turn()
    {
        mTransform.Rotate(Vector3.up, input.AimDirection.x * turnSpeed);
    }
}
