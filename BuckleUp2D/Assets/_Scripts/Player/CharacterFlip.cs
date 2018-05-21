using UnityEngine;

/// <summary>
/// Flips a character horizontally to face the direction of movement
///
/// Ruben Sanchez
/// 2/12/18
/// </summary>

public class CharacterFlip : MonoBehaviour
{
    public SpriteRenderer weaponSprite;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private float flipThreshold = .05f;

    [SerializeField]
    private Sprite downSprite;
    [SerializeField]
    private Sprite upSprite;
    [SerializeField]
    private Sprite rightSprite;
    [SerializeField]
    private Sprite upRightSprite;
    [SerializeField]
    private Sprite downRightSprite;

    private Vector3 originalScale;
    private SpriteRenderer playerRenderer;

    private int playerOrderInLayer;
    private float weaponVerticleRotation = 80;
    private float weaponDiagonalRotation = 45;


    private void Awake()
    {
        originalScale = player.transform.localScale;
        playerRenderer = player.GetComponent<SpriteRenderer>();
        playerOrderInLayer = playerRenderer.sortingOrder;
    }

    public void FaceRight()
    {
        playerRenderer.sprite = rightSprite;
        player.transform.localScale = originalScale;

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer + 1;
            weaponSprite.transform.eulerAngles = Vector3.zero;
        }
           
    }

    public void FaceLeft()
    {
        playerRenderer.sprite = rightSprite;
        player.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer - 1;
            weaponSprite.transform.eulerAngles = Vector3.zero;
        }
    }

    public void FaceUp()
    {
        playerRenderer.sprite = upSprite;

        if (weaponSprite != null)
        {
            weaponSprite.transform.eulerAngles = new Vector3(0, weaponVerticleRotation, 0);
            weaponSprite.sortingOrder = playerOrderInLayer - 1;
        }

    }

    public void FaceDown()
    {
        playerRenderer.sprite = downSprite;

        if (weaponSprite != null)
        {
            weaponSprite.transform.eulerAngles = new Vector3(0, weaponVerticleRotation, 0);
            weaponSprite.sortingOrder = playerOrderInLayer + 1;
        }
           
    }

    public void FaceUpRight()
    {
        playerRenderer.sprite = upRightSprite;
        player.transform.localScale = originalScale;

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer - 1;
            weaponSprite.transform.eulerAngles = new Vector3(weaponDiagonalRotation, weaponDiagonalRotation, 0);
        }
    }

    public void FaceDownRight()
    {
        playerRenderer.sprite = downRightSprite;
        player.transform.localScale = originalScale;

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer + 1;
            weaponSprite.transform.eulerAngles = new Vector3(0, weaponDiagonalRotation, 0);
        }
    }

    public void FaceDownLeft()
    {
        playerRenderer.sprite = downRightSprite;
        player.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer + 1;
            weaponSprite.transform.eulerAngles = new Vector3(0, -weaponDiagonalRotation, 0);
        }
    }

    public void FaceUpLeft()
    {
        playerRenderer.sprite = upRightSprite;
        player.transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);

        if (weaponSprite != null)
        {
            weaponSprite.sortingOrder = playerOrderInLayer - 1;
            weaponSprite.transform.eulerAngles = new Vector3(0, -weaponDiagonalRotation, 0);
        }
            
    }


    public void FlipCharacterToDirection(Vector3 direction)
    {
        if(Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(1, 0))) < flipThreshold)
            FaceRight();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(.7f, -.7f))) < flipThreshold)
            FaceDownRight();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(0, -1))) < flipThreshold)
            FaceDown();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(-.7f, -.7f))) < flipThreshold)
            FaceDownLeft();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(-1, 0))) < flipThreshold)
            FaceLeft();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(-.7f, .7f))) < flipThreshold)
            FaceUpLeft();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(0, 1))) < flipThreshold)
            FaceUp();

        else if (Mathf.Abs(1 - Vector3.Dot(direction, new Vector3(.7f, .7f))) < flipThreshold)
            FaceUpRight();
    }
}


