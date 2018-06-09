using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using RootMotion.FinalIK;

/// <summary>
/// Manages weapon pick ups
/// Activates Final IK offset motion
/// 
/// Ruben Sanchez
/// 6/3/18
/// </summary>
public class WeaponPickUp : MonoBehaviour
{
    [SerializeField] private UnityEvent onPickUp;

    private WeaponManager weaponManger; // WeaponManager of player that picks up this weapon

    private void OnTriggerEnter(Collider other)
    {
        // Set weaponManager to the WeaponManager on the interacting player and subscribe their OnInteract event to Equip
        if (other.gameObject.transform.root.GetComponent<WeaponManager>() != null)
        {
            weaponManger = other.gameObject.transform.root.GetComponent<WeaponManager>();
            GameManager.Instance.Input.OnInteract += Equip; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Set weaponManager to null and unsubscribe the interacting player's OnInteract event to Equip
        weaponManger = null;
        GameManager.Instance.Input.OnInteract -= Equip;
    }

    public void Equip()
    {
        if (weaponManger)
            weaponManger.Equip(GetComponent<Weapon>()); // equip weapon on the players WeaponManager

        onPickUp.Invoke();
    }
}

