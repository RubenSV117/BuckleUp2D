using UnityEngine;

/// <summary>
/// Manages player movement
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

  

    private string rollRightAnimation = "Roll_Right";
    private string rollLeftAnimation = "Roll_Left";

    private PlayerAnimation playerAnim;
    private CharacterFlip characterFlip;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        characterFlip = GetComponentInChildren<CharacterFlip>();
    }

    public void Move(Vector2 direction)
    {
        rigidB.velocity = direction * moveSpeed;
        playerAnim.Play(direction.x > 0 ? rollRightAnimation : rollLeftAnimation);
        characterFlip.FlipCharacterToDirection(direction);
    }

    public void SpeedBoost(float multiplier)
    {
        moveSpeed *= multiplier;
    }
}
