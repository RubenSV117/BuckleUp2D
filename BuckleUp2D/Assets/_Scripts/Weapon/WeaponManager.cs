using UnityEngine;

/// <summary>
/// Manages weapon equip
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class WeaponManager : MonoBehaviour
{
    public Weapon weapon;

    public void Attack(Vector2 direction)
    {
        if (weapon != null)
            weapon.Attack(direction);
    }
}
