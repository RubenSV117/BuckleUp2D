using System.Collections;
using RootMotion.FinalIK;
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
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeedMultiplier;

    [Tooltip("Added Speed")]
    [SerializeField] private float rollSpeedBoost;
    [Tooltip("In Seconds")]
    [SerializeField] private float rollLength;

    [SerializeField] private UnityEvent OnRollBegin;
    [SerializeField] private UnityEvent OnRollEnd;

    [SerializeField] private UnityEvent OnSprintBegin;
    [SerializeField] private UnityEvent OnSprintEnd;

    [SerializeField] private AimIK aimIk;
    [SerializeField] private SecondHandOnGun secondHandGun;

    [SerializeField] private Transform meshTransform;

    private InputManager input;
    private Rigidbody rigidB;
   
    private float horizontalTurnValue; // value used for spin lerping
    public bool isSprinting;
    public bool isRolling { get; private set; }
    private bool canControlMove = true;

    private Coroutine rollCoroutine;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();

        input = GameManager.Instance.Input;

        // subscribe movement methods to Input events
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
        input.OnAttack += CancelSprint;
    }

    public void Move()
    {
        if (canControlMove) // control deactivated during roll
        {
            //Vector3 moveDirection = input.MoveDirection.z * mTransform.forward; // move front/back relative to player based on Input

            //moveDirection += input.MoveDirection.x * mTransform.right; // add strafing only if not sprinting

            Vector3 moveVelocity = input.MoveDirection * (isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed); // apply speed boost if sprinting
            rigidB.velocity = new Vector2(moveVelocity.x, moveVelocity.y);
        }
    }

    public void HorizontalTurn()
    {
        if (input.AimDirection.magnitude != 0)
            meshTransform.forward = new Vector3(input.AimDirection.x, 0, input.AimDirection.y);

        else if (input.MoveDirection.magnitude != 0)
            meshTransform.forward = new Vector3(input.MoveDirection.x, 0, input.MoveDirection.y);
    }

    public void SetSprint(bool isSprinting)
    {
        if(isSprinting)
            OnSprintBegin.Invoke();

        else
            OnSprintEnd.Invoke();

        this.isSprinting = isSprinting;
    }

    public void CancelSprint()
    {
        isSprinting = false;
    }

    public void Roll()
    {
        if (rollCoroutine == null)
            rollCoroutine = StartCoroutine(RollCo());
    }

    public IEnumerator RollCo()
    {
        isRolling = true;
        OnRollBegin.Invoke();
        canControlMove = false; // deactivate control
        rigidB.velocity += Vector3.Normalize(rigidB.velocity) * rollSpeedBoost; // apply speed boost in direction of roll

        yield return new WaitForSeconds(rollLength);

        canControlMove = true;
        rollCoroutine = null;
        OnRollEnd.Invoke();
        isRolling = false;
    }

}
