using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages player movement
/// sends direction to CharacterFlip and PlayerAnimation 
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    [SerializeField] private float turnSpeed = 10f;

    [Header("Roll")]
    [SerializeField] private float rollSpeed = 20f;
    [SerializeField] private float rollDuration = 1f;
    [SerializeField] private float rollCooldown = .5f;

    [SerializeField] UnityEvent onMove;

    [SerializeField] private UnityEvent onStop;

    [SerializeField] private UnityEvent onRoll;

    private PlayerAnimation playerAnim;

    private Rigidbody rigidB;

    private int speedMultiplier = 1;

    private Coroutine rollCoroutine;
    private float rollCooldownTimer;
    private bool canRoll;


    private bool canControlMove = true;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<PlayerAnimation>();
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
            rigidB.velocity = direction * moveSpeed;
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

    public void SpeedBoost(float multiplier)
    {
        moveSpeed *= multiplier;
    }

    public void SetCanControlMove(bool control)
    {
        canControlMove = control;
    }
}
