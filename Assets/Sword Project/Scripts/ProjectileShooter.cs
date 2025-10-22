using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    [SerializeField] private float shootInterval;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform projectileSpawn;

    [SerializeField] private BaseProjectile projectile;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProjectileTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator ProjectileTimer()
    {
        ShootProjectile();

        yield return new WaitForSeconds(shootInterval);

        StartCoroutine(ProjectileTimer());
    }

    private void ShootProjectile()
    {
        BaseProjectile shotProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        shotProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * Time.deltaTime);
    }
}
