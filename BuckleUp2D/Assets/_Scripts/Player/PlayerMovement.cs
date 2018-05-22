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
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private UnityEvent onMove;

    [SerializeField] private float speedMultiplier = 2f;
    [SerializeField] private UnityEvent onSprint;

    [SerializeField] private UnityEvent onIdle;

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
            rigidB.velocity += Vector3.down * runSpeed;
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
            if(direction.magnitude > 0)
                onMove.Invoke();

            else
                onIdle.Invoke();

            float speed = direction.magnitude > .5f ? runSpeed : walkSpeed;
            
            Vector3 moveVelocity = isSprinting ? direction * speed * speedMultiplier : direction * speed;
            rigidB.velocity =  new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);

            playerAnim.SetSpeed(direction.magnitude);
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
        playerAnim.SetSprint(sprint);

        if(sprint)
            onSprint.Invoke();
    }

    public void SetCanControlMove(bool control)
    {
        canControlMove = control;
    }
}
