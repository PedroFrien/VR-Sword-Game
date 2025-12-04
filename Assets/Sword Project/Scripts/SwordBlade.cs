using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlade : MonoBehaviour
{
    [SerializeField] private float velocityMult = 1.1f;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private float bulletTimeSlow;
    [SerializeField] private float bulletTimeDuration;

    // Start is called before the first frame update

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        BaseProjectile projectile = other.gameObject.GetComponent<BaseProjectile>();
        if (projectile != null && projectile.IsParryable == true && projectile.IsParried == false)
        {
            projectile.Parry(velocityMult);

            FindObjectOfType<AudioManager>().PlaySound("Parry", transform.position, gameObject);

            FindObjectOfType<Player>().Heal(1);
            FindObjectOfType<PlayerGun>().AddEnergy(1);

            gameManager.ActivateBulletTime(bulletTimeSlow, bulletTimeDuration);

        }
    }

  
}
