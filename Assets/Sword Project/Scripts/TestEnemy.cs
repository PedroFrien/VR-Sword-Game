using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TestEnemy : BaseCharacter
{

    public float _health;


    [SerializeField] private float fireInterval;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private float pointsOnDeath = 5f;

    [SerializeField] private Transform firePoint;

    [SerializeField] private BaseProjectile bullet;

    [SerializeField] private LayerMask playerLayer;

    private Transform player;

    public Transform centerPoint;
    public float range;
    public NavMeshAgent agent;

    private bool isSearchingForPoint;
    public bool rooted;

    [SerializeField] private float parryableChance;

    [SerializeField] private string[] grunts;



    private void Start()
    {
        rooted = true;

        player = GameObject.FindGameObjectWithTag("MainCamera").transform;

        agent = GetComponent<NavMeshAgent>();

        if (isSearchingForPoint == false)
        {
            StartCoroutine(FindShootPoint());
        }
        
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            

            

            if (rooted == false)
            {
                StartCoroutine(GunTimer());
                rooted = true;
                //agent.SetDestination(transform.position);
            }
           


        }



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
        FindObjectOfType<WaveManager>().UpdatePoints(pointsOnDeath);

        int randomIndex = Random.Range(0, grunts.Length);
        string grunt = grunts[randomIndex];
        FindObjectOfType<AudioManager>().PlaySound(grunt, transform.position, gameObject);


        Destroy(gameObject);
    }

    public override float Health
    {
        get => _health;
        set => _health = value;
    }


    private IEnumerator FindShootPoint()
    {
        isSearchingForPoint = true;

        bool foundPoint = false;
        // Find a point within massive sphere

        while (foundPoint == false)
        {
            yield return null;

            Vector3 randomPoint = centerPoint.position + Random.insideUnitSphere * range; //random point in a sphere 

            NavMeshHit hit;
            var path = new NavMeshPath();

            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas) && NavMesh.CalculatePath(transform.position, randomPoint, 1, path))
            {
                Vector3 lookPoint = hit.position + new Vector3(0, 0.5f, 0);

                Vector3 playerDir = player.position - lookPoint;

                Ray ray = new Ray(lookPoint, playerDir.normalized);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, 9999, playerLayer))
                {
                    Debug.DrawRay(lookPoint, playerDir.normalized * raycastHit.distance, Color.red, 90);

                    if (raycastHit.collider.CompareTag("MainCamera"))
                    {
                        
                        Debug.Log("Clear Shot");
                        agent.SetDestination(hit.position);
                        foundPoint = true;
                        isSearchingForPoint = false;
                        rooted = false;
                    }
                    else
                    {
                        

                        Debug.Log("Not a clear shot");
                    }

                    
                }              
                
            }
            else
            {
                Debug.Log("Point not on NavMesh");
            }
            
        }




        // Sample if its on the navmesh, if not try again

        // Starting from 0.5f above the point, draw a raycast to the planet. If it is obstructed try again
    }




    private IEnumerator GunTimer()
    {
        

        while (true)
        {
            if (agent.velocity.magnitude == 0)
            {
                FireGun();
            }

            yield return new WaitForSeconds(fireInterval);
        }


        

        
    }

    private void FireGun()
    {
        transform.LookAt(player);

        FindObjectOfType<AudioManager>().PlaySound("GunShoot", transform.position, gameObject);

        BaseProjectile firedBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);

        Vector3 shotDirection = (player.position - firePoint.position).normalized;

        firedBullet.GetComponent<Rigidbody>().velocity = shotDirection * bulletSpeed;
        firedBullet.Damage = bulletDamage;
        firedBullet.RandomParry(parryableChance);



    }
}
