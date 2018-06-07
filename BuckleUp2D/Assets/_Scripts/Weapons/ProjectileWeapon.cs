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

    [SerializeField] private string basicBulletTag = "BasicBullet"; // tag for the bullet object pooling
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem impactParticle;

    [SerializeField] private UnityEvent onShoot;

    private bool onCooldown;
    private Coroutine burstFireCoroutine;

    private void OnDisable()
    {
        onCooldown = false;
        StopAllCoroutines();
        burstFireCoroutine = null;
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
        muzzleFlash.Play();

        RaycastHit hit;
        Debug.DrawRay(shootPoint.position, shootPoint.forward * 100, Color.red, 1f);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, ~layersToIgnore))
        {
            Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.transform.GetComponent<Health>())
            {
                hit.transform.GetComponent<Health>().TakeDamage(damage);
            }
        }
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
