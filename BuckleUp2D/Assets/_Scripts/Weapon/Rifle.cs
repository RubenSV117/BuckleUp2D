using System.Collections;
using UnityEngine;

/// <summary>
/// Projectile weapon, can single short or burst 
/// 
/// Ruben Sanchez
/// 
/// </summary>

public class Rifle : Weapon
 {
    public Transform shootPoint;

    [SerializeField]
    private string basicBulletTag = "BasicBullet";

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [Header("Auto Mode")]
    [Tooltip("Shot per second on auto")]
    [SerializeField] [Range(1, 20)]
    private float autoFireRate = 5f;

    [Header("Burst Mode")] [SerializeField]
    private bool burst;

    [Tooltip("Number of shots in one burst")]
    [SerializeField] [Range(1, 5)]
    private int fireBurstCount = 3;

    [Tooltip("Shots per second in a burst")]
    [SerializeField] [Range(1, 20)]
    private float fireRate = 3;

     [Tooltip("Delay in between bursts")]
     [SerializeField]
     [Range(0, 3)]
     private float burstCooldown = .5f;

    private float fireRateTimer;
    private float cooldownTimer;
    private bool onCooldown;
    private Coroutine burstFireCoroutine;

    private void Update()
    {
        if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= 1f / autoFireRate)
            {
                cooldownTimer = 0;
                onCooldown = false;
            }
        }
    }

    public override void Attack(Vector3 direction)
    {
        if (onCooldown)
            return;

        if (!burst)
        {
            onCooldown = true;
            FireSingleShot(direction);
        }

        else
        {
            onCooldown = true;
            if (burstFireCoroutine == null)
                burstFireCoroutine = StartCoroutine(BurstFire(direction));
        }
    }

    public IEnumerator BurstFire(Vector3 direction)
    {
        for (int i = 0; i < fireBurstCount; i++)
        {
            FireSingleShot(direction);
            yield return new WaitForSeconds(1 / fireRate);
        }

        yield return new WaitForSeconds(burstCooldown);
        burstFireCoroutine = null;
    }

    public void FireSingleShot(Vector3 direction)
    {
        if (muzzleFlash)
            muzzleFlash.Play();


        Projectile bullet = ObjectPooler.Instance.GetPooledObject(basicBulletTag).GetComponent<Projectile>();
        bullet.transform.position = shootPoint.position;
        bullet.transform.root.gameObject.SetActive(true);
        bullet.Shoot(transform.root.forward);
    }

    public void SetBurst(bool canBurst)
    {
        burst = canBurst;
    }

     private void OnDisable()
     {
         if (burstFireCoroutine != null)
         {
             StopCoroutine(burstFireCoroutine);
             burstFireCoroutine = null;
         }
     }
 }
