using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages player movement
/// sends direction and PlayerAnimation 
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private UnityEvent onSprint;

    //Roll
    [Header("Roll")]
    [SerializeField] private float rollSpeed = 20f;
    [SerializeField] private float rollDuration = 1f;
    [SerializeField] private float rollCooldown = .5f;
    [SerializeField] private UnityEvent onRoll;

    private Coroutine rollCoroutine;
    private float rollCooldownTimer;
    private bool canRoll;

    private Rigidbody rigidB;

    private bool canControlMove = true;

    //sprint
    private bool isSprinting;

    private PlayerAnimation playerAnim;


    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    private void Update()
    {
        if (!canRoll)
        {
            rollCooldownTimer += Time.deltaTime;

            if (rollCooldownTimer >= rollCooldown)
            {
                rollCooldownTimer = 0;
                canRoll = true;
            }
        }

        if(!GroundDetector.isGrounded)
            rigidB.velocity += Vector3.down * moveSpeed;
    }

    public void Turn(Vector3 direction)
    {
        if(canControlMove)
            transform.forward = direction;
    }

    public void Move(Vector3 direction)
    {
        if (canControlMove)
        {
            Vector3 moveDirection = isSprinting ? direction * moveSpeed * speedMultiplier : direction * moveSpeed;
            rigidB.velocity =  new Vector3(moveDirection.x, rigidB.velocity.y, moveDirection.z);
        }
    }

    public void Stop()
    {
        rigidB.velocity = Vector3.zero;
    }

    public void Roll(Vector3 direction)
    {
        if (canRoll)
        {
            canControlMove = false;
            rigidB.velocity = direction * rollSpeed;

            if (rollCoroutine == null)
                rollCoroutine = StartCoroutine(RollCo());

            onRoll.Invoke();
        }
    }

    //restrict control for duration of roll
    public IEnumerator RollCo()
    {
        yield return new WaitForSeconds(rollDuration);
        canControlMove = true;
        rollCoroutine = null;
        canRoll = false;
    }

    public void SetSprint(bool sprint)
    {
        isSprinting = sprint;

        if(sprint)
            onSprint.Invoke();

        playerAnim.SetSpeed(isSprinting ? speedMultiplier : 1);
            
    }

    public void SetCanControlMove(bool control)
    {
        canControlMove = control;
    }
}
