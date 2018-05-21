using System.Collections;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Interface for weapons
/// Sends fire direction to CharacterFlip
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public abstract class Weapon : MonoBehaviour
{
    public abstract void Attack(Vector3 direction);
}
