using System.Collections;
using UnityEngine;

/// <summary>
/// Manages behavior for the laser rifle
/// 
/// Ruben Sanchez
/// 6/8/18
/// </summary>

public class LaserWeapon : Weapon
{
    [Tooltip("Time between pressing the trigger and firing")]
    [SerializeField] private float chargeTime;
    [SerializeField] private float range;
    [SerializeField] private LineRenderer laser;
    [SerializeField] private float chargeStartLaserWidth = .2f;
    [SerializeField] private float chargeEndLaserWidth = .5f;

    private Coroutine chargeCo;

    private void Awake()
    {
        laser.enabled = false;
        GameManager.Instance.Input.OnAttackEnd += CancelCharge;
    }

    void Update()
    {
        laser.SetPosition(0, aimTransform.position);
    }

    public override void Attack()
    {
        if (chargeCo == null)
            chargeCo = StartCoroutine(Charge());
    }

    public IEnumerator Charge()
    {
        laser.endWidth = chargeStartLaserWidth;
        laser.startWidth = chargeStartLaserWidth;
        laser.enabled = true;

        float timeToShoot = Time.time + chargeTime; // time at which to shoot

        while (Time.time < timeToShoot)
        {
            RaycastHit hit;

            // update the laser position while the laser is charging
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, ~layersToIgnore))
            {
                laser.SetPosition(1, hit.point);
            }

            yield return null;
        }

        chargeCo = null;
        Shoot();

        laser.endWidth = chargeEndLaserWidth;
        laser.startWidth = chargeEndLaserWidth;
        yield return new WaitForSeconds(.5f);
        laser.enabled = false;
    }

    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, range, ~layersToIgnore))
        {

            if (hit.transform.GetComponent<Health>())
            {
                print(hit.collider.transform.root.name); 
                Health hp = hit.transform.GetComponent<Health>();
                hp.TakeDamage(hp.currentHealth);
            }
              
        }
    }

    public void CancelCharge()
    {
        if (chargeCo != null)
        {
            StopCoroutine(chargeCo);
            chargeCo = null;
            laser.enabled = false;
        }
    }
}
