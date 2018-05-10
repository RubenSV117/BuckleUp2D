using UnityEngine;

/// <summary>
/// Manages directional arrows for movement and shoot directions
/// 
/// Ruben Sanchez
/// 5/10/19
/// </summary>

public class DirectionalArrows : MonoBehaviour
{
    public GameObject moveArrow;

    public GameObject shootArrow;

    [SerializeField]
    [Range(.5f, 2)]
    private float maxScale = 2;

    public void UpdateMoveArrow(Vector2 direction)
    {
        moveArrow.transform.right = direction;
        //moveArrow.transform.localScale = scale;
    }

    public void UpdateShootArrow(Vector2 direction)
    {
        shootArrow.transform.right = direction;
        //shootArrow.transform.localScale = scale;
    }
}
