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

    [SerializeField] private float rollSpeed = 20f;

    [SerializeField] private float rollDuration = 1f;

    [SerializeField] private float rollCooldown = .5f;

    [SerializeField] UnityEvent onMove;

    [SerializeField] private UnityEvent onStop;

    [SerializeField] private UnityEvent onRoll;

    private PlayerAnimation playerAnim;

    private Rigidbody2D rigidB;

    private int speedMultiplier = 1;

    private Coroutine rollCoroutine;
    private float rollCooldownTimer;
    private bool canRoll;


    private bool canControlMove = true;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
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

    public void Move(Vector2 direction)
    {
        if (canControlMove)
        {
            rigidB.velocity = direction * moveSpeed;
        }
    }

    public void Stop()
    {
        rigidB.velocity = Vector2.zero;
    }

    public void Roll(Vector2 direction)
    {
        if (canRoll)
        {
            canControlMove = false;
            rigidB.velocity = direction * rollSpeed;

            if (rollCoroutine == null)
                rollCoroutine = StartCoroutine(RollCo(direction));


            onRoll.Invoke();
        }
    }

    //restrict control for duration of roll
    public IEnumerator RollCo(Vector2 direction)
    {
        playerAnim.PlayRoll(direction);
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
