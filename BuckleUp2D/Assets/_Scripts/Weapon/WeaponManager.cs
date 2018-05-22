using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages weapon equip
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    [SerializeField] private int maxWeapons;

    private bool canAttack = true;
    private Weapon equippedWeapon;

    private void Awake()
    {
        equippedWeapon = weapons[0];
    }

    public void Attack(Vector3 direction)
    {
        if (canAttack)
        {
            if (equippedWeapon != null)
                equippedWeapon.Attack(direction);
        }
    }

    public void SetCanAttack(bool attack)
    {
        canAttack = attack;
    }
}
