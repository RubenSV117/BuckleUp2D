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
    [SerializeField] private InputManager input;
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


    private Rigidbody rigidB;
    private Transform mTransform;
    private float horizontalTurnValue; // value used for spin lerping
    private bool isSprinting;
    private bool isRolling;
    private bool canControlMove = true;

    private Coroutine rollCoroutine;



    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        mTransform = transform;

        // subscribe movement methods to input events
        input.OnSprintChange += SetSprint;
        input.OnRoll += Roll;
    }

    private void Update()
    {
        //aimIk.enabled = (!isSprinting && !isRolling);
        //secondHandGun.enabled = (!isSprinting && !isRolling);
    }

    public void Move()
    {
        if (canControlMove) // control deactivated during roll
        {
            Vector3 moveDirection = input.MoveDirection.z * mTransform.forward; // move front/back relative to player based on input

            moveDirection += input.MoveDirection.x * mTransform.right; // add strafing only if not sprinting

            Vector3 moveVelocity = moveDirection * (isSprinting ? moveSpeed * sprintSpeedMultiplier : moveSpeed); // apply speed boost if sprinting
            rigidB.velocity = new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);
        }
    }

    public void HorizontalTurn()
    {
        if (canControlMove) // turn player horizontally based on input
        {
            horizontalTurnValue = Mathf.Lerp(horizontalTurnValue, input.AimDirection.x, Time.deltaTime * (1 / input.AimDamping.x));
            mTransform.Rotate(Vector3.up, horizontalTurnValue * input.AimSensitivity.x);
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
