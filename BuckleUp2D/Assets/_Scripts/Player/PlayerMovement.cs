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
    [SerializeField]
    private float moveSpeed = 10f;

    [SerializeField]
    private float rollSpeed = 20f;

    [SerializeField]
    private float rollDuration = 1f;

    [SerializeField]
    UnityEvent onMove;

    [SerializeField]
    private UnityEvent onStop;

    [SerializeField]
    private UnityEvent onRoll;

    private Rigidbody2D rigidB;
    private int speedMultiplier = 1;

    private CharacterFlip characterFlip;

    private Coroutine rollCoroutine;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        characterFlip = GetComponentInChildren<CharacterFlip>();
    }

    public void Move(Vector2 direction)
    {
        characterFlip.FlipCharacterToDirection(direction);
        rigidB.velocity = direction * moveSpeed;   
    }

    public void Roll(Vector2 direction)
    {
        if (rollCoroutine == null)
            rollCoroutine = StartCoroutine(RollCo(direction));
    }

    public IEnumerator RollCo(Vector2 direction)
    {
        rigidB.velocity = direction * rollSpeed;
        yield return new WaitForSeconds(rollDuration);
        rollCoroutine = null;
    }

    public void SpeedBoost(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}
