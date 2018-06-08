using UnityEngine;
using System.Collections;

/// <summary>
/// Manages Health for destructible objects
/// 
/// Ruben Sanchez
/// 6/3/18
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth;

    private int currentHealth;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
