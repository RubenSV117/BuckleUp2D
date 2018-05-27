using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls player movement
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

    void Update ()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 moveDirection = input.MoveDirection.z * mTransform.forward;
        moveDirection += input.MoveDirection.x * mTransform.right;
        moveDirection = Vector3.Normalize(moveDirection);

        rigidB.velocity = moveDirection * moveSpeed;
    }

    private void Turn()
    {
        mTransform.Rotate(Vector3.up, input.AimDirection.x * turnSpeed);
    }
}
