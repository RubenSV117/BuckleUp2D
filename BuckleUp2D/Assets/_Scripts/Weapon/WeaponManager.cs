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
    public Transform gunShootPoint;
    public ParticleSystem gunMuzzleFlash;
    public Transform aimPoint;

    [SerializeField] private int maxWeapons;
    [SerializeField] private SkinnedMeshRenderer meshRenderer;

    private bool canAttack = true;
    private Weapon equippedWeapon;

    private void Awake()
    {
        GameManager.Instance.InputManager.OnAttack += Attack;
        GameManager.Instance.InputManager.OnWeaponCycle += CycleWeapons;

        equippedWeapon = weapons[0];
        meshRenderer.sharedMesh = equippedWeapon.meshRenderer.sharedMesh;
    }

    public void Attack()
    {
        if (canAttack)
        {
            if (equippedWeapon != null)
                equippedWeapon.Attack();
        }
    }

    public void CycleWeapons()
    {
        equippedWeapon = weapons[(weapons.IndexOf(equippedWeapon) + 1) % weapons.Count];
        meshRenderer.sharedMesh = equippedWeapon.meshRenderer.sharedMesh;
    }

    public void SetCanAttack(bool attack)
    {
        canAttack = attack;
    }
}
