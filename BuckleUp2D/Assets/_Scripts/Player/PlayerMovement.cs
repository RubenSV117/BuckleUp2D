using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    [SerializeField] private Transform meshTransform;

    [SerializeField] private UnityEvent OnRollBegin;
    [SerializeField] private UnityEvent OnRollEnd;

    [SerializeField] private UnityEvent OnSprintBegin;
    [SerializeField] private UnityEvent OnSprintEnd;

    
    private Rigidbody rigidB;
   
    private float horizontalTurnValue; // value used for spin lerping
    private bool isSprinting;
    private bool canControlMove = true;

    private Coroutine rollCoroutine;



    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        meshTransform = transform;

        // subscribe movement methods to input events
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
    }

    public void Move()
    {
        if (canControlMove) // control deactivated during roll
        {
            Vector3 moveDirection = input.MoveDirection.z * meshTransform.forward; // move front/back relative to player based on input

            moveDirection += input.MoveDirection.x * meshTransform.right; // add strafing only if not sprinting

            Vector3 moveVelocity = moveDirection * (isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed); // apply speed boost if sprinting
            rigidB.velocity = new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);
        }
    }

    public void HorizontalTurn()
    {
        if (canControlMove) // turn player horizontally based on input
        {
            horizontalTurnValue = Mathf.Lerp(horizontalTurnValue, input.AimDirection.x, Time.deltaTime * (1 / input.AimDamping.x));
            meshTransform.Rotate(Vector3.up, horizontalTurnValue * input.AimSensitivity.x);
        }
    }

    public void SetSprint(bool isSprinting)
    {
        if(isSprinting)
            OnSprintBegin.Invoke();

        else
            OnSprintEnd.Invoke();

        this.isSprinting = isSprinting;
    }

    public void Roll()
    {
        if (rollCoroutine == null)
            rollCoroutine = StartCoroutine(RollCo());
    }

    public IEnumerator RollCo()
    {
        OnRollBegin.Invoke();
        canControlMove = false; // deactivate control
        rigidB.velocity += Vector3.Normalize(rigidB.velocity) * rollSpeedBoost; // apply speed boost in direction of roll

        yield return new WaitForSeconds(rollLength);

        canControlMove = true;
        rollCoroutine = null;
        OnRollEnd.Invoke();
    }

}
