using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : BaseCharacter
{

    public float _health;


    [SerializeField] private float fireInterval;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private Transform firePoint;

    [SerializeField] private BaseProjectile bullet;

    private Transform player;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        Debug.Log(player);

        StartCoroutine(GunTimer());
    }
    public override void TakeDamage(float damageTaken)
    {
        _health -= damageTaken;


        if (_health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Destroy(gameObject);
    }

    public override float Health
    {
        get => _health;
        set => _health = value;
    }


    private IEnumerator FindShootPoint()
    {
        yield return null;
    }


    private IEnumerator GunTimer()
    {
        while (true)
        {
            FireGun();

            yield return new WaitForSeconds(fireInterval);
        }


        

        
    }

    private void FireGun()
    {
        BaseProjectile firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        Vector3 shotDirection = (player.position - transform.position).normalized;

        firedBullet.GetComponent<Rigidbody>().velocity = shotDirection * bulletSpeed;
        firedBullet.Damage = bulletDamage;



    }
}
