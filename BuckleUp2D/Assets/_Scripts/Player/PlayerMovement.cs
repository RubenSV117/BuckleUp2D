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
    [SerializeField] protected Transform player;
    [SerializeField] private float runSpeed = 10f;
    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float turnSpeed = 5f;
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

    protected Rigidbody rigidB;

    private bool canControlMove = true;

    //sprint
    private bool isSprinting;

    protected PlayerAnimation playerAnim;
    private InputManager input;
    private float turnInput; 


    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
     
    }

    private void Start()
    {
        GameManager.Instance.InputManager.OnRoll += Roll;
        GameManager.Instance.InputManager.OnSprintChange += SetSprint;
        input = GameManager.Instance.InputManager;
    }

    private void Update()
    {
        Move();
        Turn();

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

    public virtual void Turn()
    {
        if (canControlMove)
        {
            turnInput = Mathf.Lerp(turnInput, input.AimDirection.x, 1f / input.AimDamping.x);
            player.Rotate(Vector3.up, turnInput * turnSpeed);
        }
            
    }

    public virtual void Move ()
    {
        if (canControlMove)
        {
            Vector3 direction = Vector3.Normalize(input.MoveDirection);

            Vector3 dirRelativeToPlayer = new Vector3();
            dirRelativeToPlayer += direction.x * player.right;
            dirRelativeToPlayer += direction.z * player.forward;
            dirRelativeToPlayer = Vector3.Normalize(dirRelativeToPlayer);

            float speed = input.MoveDirection.magnitude > .5f ? runSpeed : walkSpeed;
            Vector3 moveVelocity = isSprinting ? dirRelativeToPlayer * speed * speedMultiplier : dirRelativeToPlayer * speed;

            rigidB.velocity =  new Vector3(moveVelocity.x, rigidB.velocity.y, moveVelocity.z);

            playerAnim.SetSpeed(input.MoveDirection.magnitude);
        }
    }

    public void Stop()
    {
        rigidB.velocity = Vector3.zero;
    }

    public void Roll()
    {
        if (canRoll)
        {
            canControlMove = false;

            Vector3 direction = Vector3.Normalize(input.MoveDirection);

            Vector3 dirRelativeToPlayer = new Vector3();
            dirRelativeToPlayer += direction.x * player.right;
            dirRelativeToPlayer += direction.z * player.forward;
            dirRelativeToPlayer = Vector3.Normalize(dirRelativeToPlayer);

            rigidB.velocity = dirRelativeToPlayer * rollSpeed;

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
