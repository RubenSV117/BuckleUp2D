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
    [SerializeField] private OffsetPose holdPose; // pose to move to after pick up
    [SerializeField] private UnityEvent onPickUp;

    private WeaponManager weaponManger; // WeaponManager of player that picks up this weapon
    private InteractionObject interactionObj; // this interaction object
    private float holdWeight; // value used to smooth in the IK position
    private FullBodyBipedIK iK; // FullBodyBipedIK on the player the interacting player

    private void Awake()
    {
        interactionObj = GetComponent<InteractionObject>();
    }

    private void LateUpdate()
    {
        if (iK == null) return; // if nothing is interacting with this object

        holdPose.Apply(iK.solver, holdWeight, iK.transform.rotation); // smooth in the pose after pickup motion
    }

    private void OnTriggerEnter(Collider other)
    {
        // Set weaponManager to the WeaponManager on the interacting player and subscribe their OnInteract event to Equip
        if (other.gameObject.transform.root.GetComponent<InputManager>() != null && other.gameObject.transform.root.GetComponent<WeaponManager>() != null)
        {
            weaponManger = other.gameObject.transform.root.GetComponent<WeaponManager>();
            other.gameObject.transform.root.GetComponent<InputManager>().OnInteract += Equip; 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Set weaponManager to null and unsubscribe the interacting player's OnInteract event to Equip
        if (other.gameObject.transform.root.GetComponent<InputManager>() != null)
        {
            weaponManger = null;
            other.gameObject.transform.root.GetComponent<InputManager>().OnInteract -= Equip;
        }
    }

    public void Equip()
    {
        StartCoroutine(EquipCo());
    }

    private IEnumerator EquipCo()
    {
        iK = interactionObj.lastUsedInteractionSystem.GetComponent<FullBodyBipedIK>(); // get component on interacting player

        while (holdWeight < 1) // smooth in the offset
        {
            holdWeight += Time.deltaTime;
            yield return null;
        }


        //weaponManger.Equip(GetComponent<Weapon>()); // equip weapon on the players WeaponManager
        onPickUp.Invoke();
    }
}

