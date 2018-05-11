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
    private int currentHealth;

    [SerializeField]
    private GameObject body;

    [SerializeField]
    private UnityEvent onDeath;

    void Start ()
	{
	    currentHealth = startingHealth;
	}

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            onDeath.Invoke();
            body.SetActive(false);
            currentHealth = startingHealth;
        }
    }
}
