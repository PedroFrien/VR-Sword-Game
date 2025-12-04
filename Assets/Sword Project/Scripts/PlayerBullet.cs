using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private int maxBounces;
    [SerializeField] private int currentBounces = 0;

    public float speed;

    [SerializeField] private Rigidbody rb;

    private List<GameObject> enemies;
    private WaveManager waveManager;

    private GameObject closestEnemy;
    private float closestDistance;
    private Vector3 dirToEnemy;
    private void Start()
    {
        waveManager = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<WaveManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        BaseCharacter character = collision.gameObject.GetComponent<BaseCharacter>();

        currentBounces++;

        if (currentBounces >= maxBounces)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        BaseCharacter character = other.gameObject.GetComponent<BaseCharacter>();

        currentBounces++;
        if (character != null)
        {
            character.TakeDamage(5);

            Bounce();
        }

        if (currentBounces >= maxBounces)
        {
            Destroy(gameObject);
        }
    }

    private void Bounce()
    {
        enemies = waveManager.spawnedEnemies;

        closestEnemy = null;
        closestDistance = 0;
        dirToEnemy = Vector3.zero;

        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                float distance = Vector3.Distance(transform.position, enemy.transform.position);
                dirToEnemy = (enemy.transform.position - transform.position).normalized;

                if (distance < closestDistance && CanHit(enemy, dirToEnemy))
                {
                    

                    closestDistance = distance;
                    closestEnemy = enemy;
                }
            }
        }


        if (closestEnemy != null)
        {
            dirToEnemy = (closestEnemy.transform.position - transform.position).normalized;
            rb.velocity = Vector3.zero;
            rb.velocity = dirToEnemy * speed;
        }

        

    }

    private bool CanHit(GameObject enemy, Vector3 enemyDir)
    {
        Ray ray = new Ray(transform.position, enemyDir);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 100))
        {
            if (raycastHit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Found path");
                return true;
            }
            
        }

        Debug.Log("Did not find path");
        return false;
        
    }
}

    
