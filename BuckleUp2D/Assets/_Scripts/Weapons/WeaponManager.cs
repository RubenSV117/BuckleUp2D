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
    [SerializeField] private List<Weapon> weapons;

    [SerializeField] private int maxWeapons;
    [SerializeField] private InputManager input;
    [SerializeField] private Transform attachPoint;

    private Weapon equippedWeapon;

    private void Awake()
    {
        input.OnAttack += Attack;
        input.OnWeaponCycle += CycleWeapon;
        equippedWeapon = weapons[0];
    }

    public void Equip(Weapon w)
    {
        // add weapon to the list if not full and disable it
        if(weapons.Count < maxWeapons)
            weapons.Add(w);

        //parent new weapon to attach point and disable it
        w.transform.SetParent(attachPoint);
        w.transform.localPosition = Vector3.zero;
        w.transform.localEulerAngles = Vector3.zero;
        w.gameObject.SetActive(false);
    }

    public void CycleWeapon()
    {
        if (weapons.Count <= 1)
            return;

        // disable the equipped weapon
        //equippedWeapon.gameObject.SetActive(false);

        // cycle equipped weapon
        equippedWeapon = weapons[(weapons.IndexOf(equippedWeapon) + 1) % weapons.Count];

        // enable new equipped weapon
       // equippedWeapon.gameObject.SetActive(true);
    }

    public void Attack()
    {
        equippedWeapon.Attack();
    }



}
