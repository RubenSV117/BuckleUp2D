using System.Collections;
using UnityEngine;

/// <summary>
/// Reactivates object
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class Respawn : MonoBehaviour
 {
     [SerializeField]
     private float respawnTime;

     private Coroutine respawnCoroutine;

     public void RespawnObject()
     {
         if (respawnCoroutine == null)
             respawnCoroutine = StartCoroutine(RespawnCo());
     }

     public IEnumerator RespawnCo()
     {
         yield return new WaitForSeconds(respawnTime);
         gameObject.SetActive(true);
     }

}
