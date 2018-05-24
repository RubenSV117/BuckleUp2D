using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Reactivates object
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class RespawnManager : MonoBehaviour
 {
     [SerializeField]
     private float respawnTime = 2;

     [SerializeField]
     private GameObject body;

     [SerializeField]
     private UnityEvent onRespawn;

     private Coroutine respawnCoroutine;

     public void RespawnObject()
     {
         if (respawnCoroutine == null)
             respawnCoroutine = StartCoroutine(RespawnCo());
     }

     public IEnumerator RespawnCo()
     {
         yield return new WaitForSeconds(respawnTime);
         body.SetActive(true);
         respawnCoroutine = null;
         onRespawn.Invoke();
     }

}
