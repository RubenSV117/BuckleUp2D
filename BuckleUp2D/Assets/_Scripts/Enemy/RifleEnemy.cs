using System.Collections;
using UnityEngine;

/// <summary>
/// Manages Rifle enemy weapon behavior
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class RifleEnemy : Enemy
 {
     [SerializeField]
     private float fireCooldown = 2;

     private GameObject player;
     private Rifle rifle;

     private Coroutine shootCoroutine;

     void Start()
     {
         player = FindObjectOfType<PlayerMovement>().gameObject;
         rifle = GetComponent<Rifle>();
     }

     private void Update()
     {
         if (shootCoroutine == null)
             shootCoroutine = StartCoroutine(ShootPlayer());
     }

     public IEnumerator ShootPlayer()
     {
         Vector3 direction = Vector3.Normalize(player.transform.position - rifle.shootPoint.position);
         Attack(direction);
         yield return new WaitForSeconds(fireCooldown);
         shootCoroutine = null;
     }

     public void StopShot()
     {
         if (shootCoroutine != null)
         {
             StopCoroutine(shootCoroutine);
             shootCoroutine = null;
         }
     }

     public override void Attack(Vector3 direction)
     {
         rifle.FireSingleShot(direction);
     }
}
