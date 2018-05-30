using System.Collections;
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
    [SerializeField] private float sprintSpeedMultiplier;

    [Tooltip("Added Speed")]
    [SerializeField] private float rollSpeedBoost;
    [Tooltip("In Seconds")]
    [SerializeField] private float rollLength;

    private Rigidbody rigidB;
    private Transform mTransform;

    private float horizontalTurnValue; // value used for spin lerping
    private bool isSprinting;
    private bool canControlMove = true;

    private Coroutine rollCoroutine;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        mTransform = transform;
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
    }

    public void Move()
    {
        if (canControlMove)
        {
            Vector3 moveDirection = input.MoveDirection.z * mTransform.forward;

            if (!isSprinting)
                moveDirection += input.MoveDirection.x * mTransform.right;

            Vector3 moveVelocity = moveDirection * (isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed);
            rigidB.velocity = new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);
        }
    }

    public void HorizontalTurn()
    {
        if (canControlMove)
        {

            horizontalTurnValue = Mathf.Lerp(horizontalTurnValue, input.AimDirection.x, Time.deltaTime * (1 / input.AimDamping.x));
            mTransform.Rotate(Vector3.up, horizontalTurnValue * input.AimSensitivity.x);
        }
    }

    public void SetSprint(bool isSprinting)
    {
        this.isSprinting = isSprinting;
    }

    public void Roll()
    {
        if (rollCoroutine == null)
            rollCoroutine = StartCoroutine(RollCo());
    }

    public IEnumerator RollCo()
    {
        canControlMove = false;
        rigidB.velocity += Vector3.Normalize(rigidB.velocity) * rollSpeedBoost;

        yield return new WaitForSeconds(rollLength);

        canControlMove = true;
        rollCoroutine = null;
    }

}
