using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Defines behavior for projectile weapons, can auto or burst fire
/// 
/// Ruben Sanchez
/// 6/3/18
/// </summary>
public class ProjectileWeapon : Weapon
{
    public Transform shootPoint;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int maxReloadAmmo;
    [SerializeField] private int maxClipAmmo;
    [SerializeField] private LayerMask layersToIgnore;

    [Header("Auto Mode")]
    [Tooltip("Shot per second on auto")]
    [SerializeField]
    [Range(1, 20)]
    private float autoFireRate = 5f;

    [Header("Burst Mode")]
    [SerializeField]
    private bool burst;

    [Tooltip("Number of shots in one burst")]
    [SerializeField]
    [Range(1, 5)]
    private int fireBurstCount = 3;

    [Tooltip("Shots per second in a burst")]
    [SerializeField]
    [Range(1, 20)]
    private float fireRate = 3;

    [Tooltip("Delay in between bursts")]
    [SerializeField]
    [Range(0, 3)]
    private float burstCooldown = .5f;

    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem impactParticle;

    [SerializeField] private UnityEvent onShoot;

    private bool onCooldown;
    private Coroutine burstFireCoroutine;

    private float currentReloadAmmo;
    private float currentClipAmmo;

    private void OnDisable()
    {
        onCooldown = false;
        StopAllCoroutines();
        burstFireCoroutine = null;
    }

    private void Awake()
    {
        currentReloadAmmo = maxReloadAmmo;
        currentClipAmmo = maxClipAmmo;
    }

    public override void Attack()
    {
        if (onCooldown)
            return;

        onShoot.Invoke();

        // if automatic, fire single shot and begin cooldown coroutine
        if (!burst)
        {
            onCooldown = true;
            FireSingleShot();
            StartCoroutine(AutoFireCoolDown());
        }

        // if burst, begin the burst fire coroutine
        else
        {
            onCooldown = true;

            if (burstFireCoroutine == null)
                burstFireCoroutine = StartCoroutine(BurstFire());
        }
    }

    private void FireSingleShot()
    {
        // if weapon still has ammo in the clip
        if (currentClipAmmo > 0) 
        {
            muzzleFlash.Play();

            currentClipAmmo--;

            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, ~layersToIgnore))
            {
                Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));

                if (hit.transform.GetComponent<Health>())
                {
                    hit.transform.GetComponent<Health>().TakeDamage(damage);
                }
            }

            // if you're out of ammo in the clip but have enough to reload
            if (currentClipAmmo == 0 && currentReloadAmmo > 0) 
                Reload();
        }
        
    }

    public void Reload()
    {
        // if you still have enough ammo for a full clip
        if (currentReloadAmmo > maxClipAmmo)
        {
            currentReloadAmmo -= (maxClipAmmo - currentClipAmmo); // subtract only the ammo missing from the clip
            currentClipAmmo = maxClipAmmo;
        }
        
        // else reload the rest of the ammo
        else
        {
            currentClipAmmo = currentReloadAmmo;
            currentReloadAmmo = 0;
        }
    }

    public void AddAmo(int amount)
    {
        // if amount would fill up your max
        if (currentReloadAmmo + amount >= maxReloadAmmo)
            currentReloadAmmo = maxReloadAmmo;

        // else just add the amount
        else
            currentReloadAmmo += amount;
    }


    public IEnumerator BurstFire()
    {
        // fire burst count with set delay in between
        for (int i = 0; i < fireBurstCount; i++)
        {
            FireSingleShot();
            yield return new WaitForSeconds(1 / fireRate);
        }

        // wait for the next burst to be available
        yield return new WaitForSeconds(burstCooldown);
        onCooldown = false;
        burstFireCoroutine = null;
    }

    public IEnumerator AutoFireCoolDown()
    {
        // wait for the next shot to be available
        yield return new WaitForSeconds(1f / autoFireRate);
        onCooldown = false;
    }


}
