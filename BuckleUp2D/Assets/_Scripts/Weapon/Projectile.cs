using System.Collections;
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
    private float damage;
 
    private Rigidbody2D rigidB;
    private Coroutine disableCoroutine;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody2D>();
    }

    public void Shoot(Vector2 direction)
    {
        rigidB.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        gameObject.SetActive(false);
    }
}
