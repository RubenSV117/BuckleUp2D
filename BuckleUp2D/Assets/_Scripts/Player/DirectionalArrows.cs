using UnityEngine;

/// <summary>
/// Manages directional arrows for movement and shoot directions
/// 
/// Ruben Sanchez
/// 5/10/19
/// </summary>

public class DirectionalArrows : MonoBehaviour
{
    public static DirectionalArrows Instance;
    public GameObject moveArrow;
    public GameObject shootArrow;

    [SerializeField]
    [Range(.5f, 2)]
    private float maxScale = 2;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateMoveArrow(Vector2 direction)
    {
        moveArrow.transform.right = direction;
    }

    public void UpdateShootArrow(Vector2 direction)
    {
        shootArrow.transform.right = direction;
    }
}
