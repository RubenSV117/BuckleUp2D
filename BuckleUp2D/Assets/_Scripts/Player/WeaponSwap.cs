using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// Swaps parenting between equipped and unequipped weapons for the weapon swap animation
/// Fixes position and rotation of the weapons afterwards
/// 
/// Ruben Sanchez
/// 6/5/18
/// </summary>

[Serializable]
public class WeaponModel
{
    public bool isEquipped;
    public Transform model;
}


public class WeaponSwap : MonoBehaviour
{
    [SerializeField] private WeaponModel[] weapons;

    [SerializeField] private Transform equippedWeaponPivot;
    [SerializeField] private Transform unequippedWeaponPivot;

    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;

    // parent unequipped weapon to the right hand to bring it forward in the animation
    public void ParentUnequipped()
    {
        foreach (WeaponModel w in weapons)
        {
            if(!w.isEquipped)
                w.model.SetParent(rightHand);
        }
    }

    // parent equipped weapon to the left hand to bring it up and over to the back in the animation
    public void ParentEquipped()
    {
        foreach (WeaponModel w in weapons)
        {
            if (w.isEquipped)
                w.model.SetParent(leftHand);
        }
    }

    public void FixWeaponsPosAndRot()
    {
        foreach (WeaponModel w in weapons)
        {
            w.model.SetParent(w.isEquipped ? equippedWeaponPivot : unequippedWeaponPivot);
                   
            w.model.localPosition = Vector3.zero;
            w.model.localEulerAngles = Vector3.zero;
        
        }
    }

}
