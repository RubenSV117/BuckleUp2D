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

    private WeaponManager weaponManager;
    private Vector2 originalScale;

    private void Awake()
    {
        originalScale = weaponRoot.transform.localScale;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            StartCoroutine(Equip(other.gameObject));
    }

    public IEnumerator Equip(GameObject obj)
    {
        yield return new WaitForSeconds(.1f);
        Animator anim = obj.GetComponentInParent<Animator>();
        
        ////wait for idle
        //if (anim.GetCurrentAnimatorStateInfo(0).IsName(stateToWaitFor))
        //{
        //    while (!anim.GetCurrentAnimatorStateInfo(0).IsName(stateToWaitFor))
        //    {
        //        yield return null;
        //    }
        //}


        weaponManager = obj.transform.root.GetComponentInChildren<WeaponManager>(); // get weapon manager on the player
        weaponManager.weapon = GetComponentInParent<Weapon>(); // set weapon manager's weapon to this
        weaponRoot.root.SetParent(obj.transform.root.GetComponentInChildren<CharacterFlip>().transform); // parent weapon

        CharacterFlip characterFlip = obj.transform.root.GetComponentInChildren<CharacterFlip>();
        characterFlip.weaponSprite = GetComponent<SpriteRenderer>();

        weaponRoot.position = weaponManager.transform.position;
        weaponRoot.eulerAngles = Vector3.zero;
        weaponRoot.localScale = originalScale;

        onPickup.Invoke();
    }

}
