using System.Collections;
using UnityEngine;

/// <summary>
/// Base enemy class
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public abstract class Enemy : MonoBehaviour
{
   public abstract void Attack(Vector3 direction);
}
