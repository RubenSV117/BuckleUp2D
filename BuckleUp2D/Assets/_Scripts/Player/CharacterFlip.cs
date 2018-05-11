using UnityEngine;

/// <summary>
/// Flips a character horizontally to face the direction of movement
///
/// Ruben Sanchez
/// 2/12/18
/// </summary>

public class CharacterFlip : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Vector2 originalScale;

    public bool facingRight { get; private set; }

    private void Awake()
    {
        originalScale = player.transform.localScale;
    }

    public void FaceRight()
    {
        player.transform.localScale = originalScale;
        facingRight = true;

    }

    public void FaceLeft()
    {
        player.transform.localScale = new Vector2(-originalScale.x, originalScale.y);
        facingRight = false;
    }

    public void FlipCharacterToDirection(Vector2 direction)
    {
        if(direction.x > 0)
            FaceRight();

        else
            FaceLeft();
    }
}


