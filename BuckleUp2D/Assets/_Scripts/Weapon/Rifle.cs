﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Projectile weapon, can single short or burst 
/// Ruben Sanchez
/// 
/// </summary>

public class Rifle : Weapon
 {
    public Transform shootPoint;

    [SerializeField]
    private string basicBulletTag = "BasicBullet";

    [Tooltip("Cooldown between single shots or bursts")]
    [SerializeField]
    [Range(.2f, 2)]
    private float cooldown = .2f;

    [Header("Burst Attack")]
    [SerializeField]
    private bool burst;

    [Tooltip("Number of shots in one burst")]
    [SerializeField]
    [Range(1, 5)]
    private int fireBurstCount = 3;

    [Tooltip("Shots per second")]
    [SerializeField]
    [Range(1, 10)]
    private float fireRate = 3;

    private float fireRateTimer;
    private float cooldownTimer;
    private bool onCooldown;
    private Coroutine burstFireCoroutine;

    private void Update()
    {
        if (onCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= cooldown)
                onCooldown = false;
        }
    }

    public override void Attack(Vector2 direction)
    {
        if (onCooldown)
            return;

        if (characterFlip != null) characterFlip.FlipCharacterToDirection(direction);

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
