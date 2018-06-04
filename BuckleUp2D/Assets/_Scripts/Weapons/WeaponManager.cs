using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages weapon inventory and cycling
/// 
/// Ruben Sanchez
/// 6/3/18 
/// </summary>
public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int maxWeapons;
    [SerializeField] private InputManager input;
    [SerializeField] private List<Weapon> weapons;
    [SerializeField] private Transform attachPoint;

    private Weapon equippedWeapon;

    private void Awake()
    {
        input.OnAttack += Attack;
        equippedWeapon = weapons[0];
    }

    public void Equip(Weapon w)
    {
        // add weapon to the list if not full and disable it
        if(weapons.Count < maxWeapons)
            weapons.Add(w);

        w.transform.SetParent(attachPoint);
        w.transform.localPosition = Vector3.zero;
        w.transform.localEulerAngles = Vector3.zero;

        w.gameObject.SetActive(false);
    }

    public void CycleWeapon()
    {
        // disable the equipped weapon, set it to the next index on the list and enable the new weapon
        equippedWeapon.gameObject.SetActive(false);
        equippedWeapon = weapons[(weapons.IndexOf(equippedWeapon) + 1) % weapons.Count];
        equippedWeapon.gameObject.SetActive(true);
    }

    public void Attack()
    {
        if (equippedWeapon)
            equippedWeapon.Attack();
    }



}
