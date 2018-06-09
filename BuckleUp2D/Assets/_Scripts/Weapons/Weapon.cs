using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all weapons
/// 
/// Ruben Sanchez
/// 6/3/18
/// </summary>

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack();
    public Transform aimTransform; // transform used for aiming IK

    [SerializeField] protected LayerMask layersToIgnore;
}
