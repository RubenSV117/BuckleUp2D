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
    [SerializeField] protected GameObject weaponModel;

    public abstract void Attack();
}
