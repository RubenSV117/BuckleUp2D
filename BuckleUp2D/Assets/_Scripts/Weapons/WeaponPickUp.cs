using UnityEngine;

/// <summary>
/// Manages weapon pick ups
/// 
/// Ruben Sanchez
/// 6/3/18
/// </summary>
public class WeaponPickUp : MonoBehaviour
{
    private WeaponManager weaponManger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.root.GetComponent<InputManager>() != null && other.gameObject.transform.root.GetComponent<WeaponManager>() != null)
        {
            weaponManger = other.gameObject.transform.root.GetComponent<WeaponManager>();
            other.gameObject.transform.root.GetComponent<InputManager>().OnInteract += Equip; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.root.GetComponent<InputManager>() != null)
        {
            weaponManger = null;
            other.gameObject.transform.root.GetComponent<InputManager>().OnInteract -= Equip;
        }
    }

    public void Equip()
    {
        weaponManger.Equip(GetComponent<Weapon>());
    }
}

