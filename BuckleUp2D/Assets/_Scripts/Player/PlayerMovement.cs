using UnityEngine;

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
    private float moveSpeed;

    private Rigidbody2D rigidB;
    private int speedMultiplier = 1;

    private PlayerAnimation playerAnim;
    private CharacterFlip characterFlip;

    private bool isMovingAnim;


    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        characterFlip = GetComponentInChildren<CharacterFlip>();
    }

    public void Move(Vector2 direction)
    {
        characterFlip.FlipCharacterToDirection(direction);
        rigidB.velocity = direction * moveSpeed;

        if (!isMovingAnim)
        {
            isMovingAnim = true;
            playerAnim.PlayWalk(direction);
        }
    
    }

    public void SetMoving(bool isMoving)
    {
        isMovingAnim = isMoving;
    }

    public void SpeedBoost(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}
