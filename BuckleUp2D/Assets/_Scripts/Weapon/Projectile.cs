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
    private int damage;
 
    private Rigidbody rigidB;
    private float timeToDisable = 3;

    private void Awake()
    {
        rigidB = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if(GetComponent<TrailRenderer>())
            GetComponent<TrailRenderer>().Clear();

       StartCoroutine(DisableObject());
    }

    public void Shoot(Vector3 direction)
    {
        rigidB.velocity = direction * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.root.gameObject.GetComponent<Health>() != null)
            other.transform.root.gameObject.GetComponent<Health>().TakeDamage(damage);

        gameObject.SetActive(false);
    }

    public IEnumerator DisableObject()
    {
        yield return new WaitForSeconds(timeToDisable);

        if(gameObject.activeInHierarchy)
            gameObject.SetActive(false);
    }
}
