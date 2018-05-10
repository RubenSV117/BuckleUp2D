using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic health script
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class Health : MonoBehaviour
{
    [SerializeField]
    private int startingHealth = 3;

    [SerializeField]
    private UnityEvent onDeath;

    private int currentHealth;

    void Start ()
	{
	    currentHealth = startingHealth;
	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            onDeath.Invoke();
        }

    }
}
