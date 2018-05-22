using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages weapon pickup
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class PickUp : MonoBehaviour
{
    [SerializeField]
    private Transform weaponRoot;

    [SerializeField]
    private UnityEvent onPickup;

    private string playerTag = "Player";
    private string stateToWaitFor = "Idle";

    private WeaponManager weaponManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
            StartCoroutine(Equip(other.gameObject));
    }

    public IEnumerator Equip(GameObject obj)
    {
        yield return new WaitForSeconds(.1f);      

        weaponManager = obj.transform.root.GetComponentInChildren<WeaponManager>(); // get weapon manager on the player
        weaponManager.weapons.Add(GetComponentInParent<Weapon>()); // set weapon manager's weapon to this
        GetComponentInParent<Weapon>().Equip(weaponManager); // equip weapon
        transform.root.SetParent(weaponManager.transform);

        onPickup.Invoke();
    }

}
