using System.Collections;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            StartCoroutine(Equip(other.gameObject));
    }

    public IEnumerator Equip(GameObject obj)
    {
        Animator anim = obj.GetComponentInParent<Animator>();

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName(stateToWaitFor))
        {
            yield return null;
        }

        transform.localScale = obj.transform.root.GetComponentInChildren<CharacterFlip>().facingRight
            ? transform.localScale
            : new Vector3(-transform.localScale.x, weaponRoot.transform.localScale.y);

        WeaponManager wp = obj.transform.root.GetComponentInChildren<WeaponManager>(); // get weapon manager on the player
        wp.weapon = GetComponentInParent<Weapon>(); // set weapon manager's weapon to this
        weaponRoot.root.SetParent(wp.transform); // parent weapon
        GetComponentInParent<Weapon>().characterFlip = obj.transform.root.GetComponentInChildren<CharacterFlip>();
        transform.position = wp.transform.position;
        weaponRoot.eulerAngles = Vector3.zero;

 

        onPickup.Invoke();



    }

}
