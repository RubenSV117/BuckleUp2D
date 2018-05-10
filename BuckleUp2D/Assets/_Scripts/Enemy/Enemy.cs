using System.Collections;
using UnityEngine;

/// <summary>
/// Base enemy class
/// 
/// Ruben Sanchez
/// 5/10/18
/// </summary>

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float fireCooldown = 2;

    private GameObject player;
    private Gun gun;

    private Coroutine shootCoroutine;

	void Start ()
	{
	    player = FindObjectOfType<PlayerMovement>().gameObject;
	    gun = GetComponent<Gun>();
	}

    private void Update()
    {
        if (shootCoroutine == null)
            shootCoroutine = StartCoroutine(ShootPlayer());
    }

    public IEnumerator ShootPlayer()
    {
        print("trying to fire");
        gun.FireSingleShot(player.transform.position - gun.shootPoint.position);
        yield return new WaitForSeconds(fireCooldown);
        shootCoroutine = null;
    }
}
