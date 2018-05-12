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

    [SerializeField]
    private float verticalFlipThreshold = .8f;

    [SerializeField]
    private Sprite downSprite;
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite sideSprite;

    private Vector2 originalScale;
    private SpriteRenderer playerRenderer;

    private void Awake()
    {
        originalScale = player.transform.localScale;
        playerRenderer = player.GetComponent<SpriteRenderer>();
    }

    public void FaceRight()
    {
        playerRenderer.sprite = sideSprite;
        player.transform.localScale = originalScale;
    }

    public void FaceLeft()
    {
        playerRenderer.sprite = sideSprite;
        player.transform.localScale = new Vector2(-originalScale.x, originalScale.y);
    }

    public void FaceUp()
    {
        playerRenderer.sprite = upSprite;
    }

    public void FaceDown()
    {
        playerRenderer.sprite = downSprite;
    }

    public void FlipCharacterToDirection(Vector2 direction)
    {
        if(direction.x > 0 && Mathf.Abs(direction.y) < verticalFlipThreshold)
            FaceRight();

        else if(direction.x < 0 && Mathf.Abs(direction.y) < verticalFlipThreshold)
            FaceLeft();

        else if(direction.y > verticalFlipThreshold)
            FaceUp();

        else if(direction.y < -verticalFlipThreshold)
            FaceDown();
    }
}


