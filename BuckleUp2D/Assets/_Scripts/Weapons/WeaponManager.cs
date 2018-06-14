using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RootMotion.FinalIK;

/// <summary>
/// Manages weapon inventory and cycling
/// 
/// Ruben Sanchez
/// 6/3/18 
/// </summary>
public class WeaponManager : MonoBehaviour
{
    public List<Weapon> weapons;

    [SerializeField] private int maxWeapons;
    [SerializeField] private Transform attachPoint; // attach point for unequipped weapon

    private WeaponModelSwap modelSwap; // on the animator object, used for animation events during weapon swap
    private TouchInputManager input;
    private Weapon equippedWeapon;

    private void Awake()
    {
        modelSwap = GetComponentInChildren<WeaponModelSwap>();

        equippedWeapon = weapons[0];

        input = GameManager.Instance.Input;

        // subscribe events 
        input.OnAttack += Attack;
        input.OnWeaponCycle += CycleWeapon;
    }

    public void Equip(Weapon w)
    {
        // add weapon to the list if not full and disable it
        if (weapons.Count < maxWeapons)
        {
            modelSwap.weapons.Add(w.transform); // add to the list for WeaponModelSwap on the animator object
            weapons.Add(w); // add Weapon script to this list 

            //parent new weapon to attach point and disable it
            w.transform.GetComponent<Rigidbody>().isKinematic = true;
            w.transform.SetParent(attachPoint);
            w.transform.localPosition = Vector3.zero;
            w.transform.localEulerAngles = Vector3.zero;
        }
    }

    public void CycleWeapon()
    {
        if (weapons.Count <= 1)
            return;

        // cycle equipped weapon
        equippedWeapon = weapons[(weapons.IndexOf(equippedWeapon) + 1) % weapons.Count];
    }

    public void Attack()
    {
        equippedWeapon.Attack();
    }

}
