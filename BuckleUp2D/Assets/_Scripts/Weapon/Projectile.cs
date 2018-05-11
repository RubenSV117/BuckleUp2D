﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Base class for projectiles
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private int damage;
 
    private Rigidbody2D rigidB;
    private Coroutine disableCoroutine;
    private float timeToDisable = 3;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (disableCoroutine == null)
            disableCoroutine = StartCoroutine(DisableObject());
    }

    public void Shoot(Vector2 direction)
    {
        rigidB.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponentInParent<Health>() != null)
            other.gameObject.GetComponentInParent<Health>().TakeDamage(damage);

        else
        {
            print("reee");
        }

        gameObject.SetActive(false);
    }

    public IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(timeToDisable);
        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);

        disableCoroutine = null;
    }
}
