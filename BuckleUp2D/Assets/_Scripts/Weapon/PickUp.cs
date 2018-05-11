using UnityEngine;

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

    private string playerTag = "Player";
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            print("ree");
            transform.root.SetParent(other.transform);
            WeaponManager wp = other.transform.root.GetComponentInChildren<WeaponManager>();
            wp.weapon = GetComponentInParent<Weapon>();
            transform.position = wp.transform.position;
            weaponRoot.eulerAngles = Vector3.zero;
        }
    }
}
