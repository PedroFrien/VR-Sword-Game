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

        while (true)
        {
            ShootProjectile();

            yield return new WaitForSeconds(shootInterval);
        }
        

    }

    private void ShootProjectile()
    {
        BaseProjectile shotProjectile = Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
        shotProjectile.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed * Time.deltaTime;
    }
}
