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

    private bool canAttack = true;

    public void Attack(Vector2 direction)
    {
        if (canAttack)
        {
            if (weapon != null)
                weapon.Attack(direction);
        }
    }

    public void SetCanAttack(bool attack)
    {
        canAttack = attack;
    }
}
