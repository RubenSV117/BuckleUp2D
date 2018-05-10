using System.Collections;
using System.Reflection;
using UnityEngine;

/// <summary>
/// Base class for weapons, can do single shot or burst fire
/// Sends fire direction to CharacterFlip
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private Transform shootPoint;

    [Tooltip("Cooldown between single shots or bursts")]
    [SerializeField]
    [Range(.2f, 2)]
    private float cooldown = .2f;

    [Header("Burst Fire")]
    [SerializeField]
    private bool burst;

    [Tooltip("Number of shots in one burst")]
    [SerializeField]
    [Range(1, 5)]
    private int fireBurstCount;

    [Tooltip("Shots per second")]
    [SerializeField]
    [Range(1, 5)]
    private int fireRate;

    private float fireRateTimer;
    private float cooldownTimer;
    private bool onCooldown;
    private Coroutine burstFireCoroutine;

    private CharacterFlip characterFlip;

    private string basicBulletTag = "BasicBullet";

    private void Awake()
    {
        characterFlip = GetComponentInChildren<CharacterFlip>();
    }

    private void Update()
    {
        if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= cooldown)
                onCooldown = false;
        }
    }

    public void Fire(Vector2 direction)
    {
        if (onCooldown)
            return;

        characterFlip.FlipCharacterToDirection(direction);

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

    public IEnumerator BurstFire(Vector2 direction)
    {
        for (int i = 0; i < fireBurstCount; i++)
        {
            FireSingleShot(direction);
            yield return new WaitForSeconds(1 / fireRate);
        }

        burstFireCoroutine = null;
    }

    public void FireSingleShot(Vector2 direction)
    {
        Projectile bullet = ObjectPooler.Instance.GetPooledObject(basicBulletTag).GetComponent<Projectile>();
        bullet.transform.position = shootPoint.position;
        bullet.gameObject.SetActive(true);
        bullet.Shoot(direction);
    }

    public void SetBurst(bool canBurst)
    {
        burst = canBurst;
    }
}
