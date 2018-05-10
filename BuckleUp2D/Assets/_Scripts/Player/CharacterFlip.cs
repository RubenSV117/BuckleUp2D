using UnityEngine;

/// <summary>
/// Flips a character to face the direction of movement
///
/// Ruben Sanchez
/// 2/12/18
/// </summary>

public class CharacterFlip : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Rigidbody2D rigidB;
    private Vector2 originalScale;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();

        originalScale = player.transform.localScale;
    }

    private void Update()
    {
        if (rigidB.velocity.x < 0)
            player.transform.localScale = new Vector2(-originalScale.x, originalScale.y);

        if (rigidB.velocity.x > 0)
            player.transform.localScale = originalScale;
    }

    public void FlipCharacterToDirection(Vector2 direction)
    {
        if(direction.x > 0)
            player.transform.localScale = originalScale;

        else
            player.transform.localScale = new Vector2(-originalScale.x, originalScale.y);
    }
}


