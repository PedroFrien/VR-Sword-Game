using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Bullet : BaseProjectile
{
    public bool _isParryable;
    public bool _isParried;
    public float _damage;

    public Rigidbody rb;

    private Vector3 previousPosition;

    [SerializeField] private float bulletRadius;
    [SerializeField] private LayerMask swordLayer;
    [SerializeField] private LayerMask projectileLayer;

    [SerializeField] private Material parryableMat;
    [SerializeField] private Material nonParryableMat;

    [SerializeField] private float destroyProjectileDistance;


    void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = (transform.position - previousPosition).normalized;
        

        //if (Physics.SphereCast(previousPosition, bulletRadius, direction.normalized, out RaycastHit hit, distance, swordLayer))
        //{
        //    Parry(2);
        //}


        if (_isParried)
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, destroyProjectileDistance, projectileLayer))
            {
                if (hit.collider.GetComponent<BaseProjectile>() != null)
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        BaseCharacter hitCharacter = collision.gameObject.GetComponent<BaseCharacter>();
        if (hitCharacter != null)
        {
            hitCharacter.TakeDamage(_damage);
        }


        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsParryable && other.GetComponent<BaseProjectile>() != null)
        {
            Destroy(other.gameObject);
        }
    }

    public override void Parry(float VelocityMult)
    {
        rb.velocity = rb.velocity * VelocityMult * -1;
        _isParried = true;
    }

    public override void RandomParry(float parryChance)
    {
        int randomNum = Random.Range(0, 100);

        if (randomNum <= parryChance)
        {
            IsParryable = true;
            GetComponent<MeshRenderer>().material = parryableMat;
        }
        else
        {
            IsParryable = false;
            GetComponent<MeshRenderer>().material = nonParryableMat;
        }



    }

    public override bool IsParryable 
    {
        get { return _isParryable; }
        set
        {
            _isParryable = value;

        }
    }

    public override bool IsParried
    {
        get { return _isParried; }
        set
        {
            _isParried = value;

        }
    }

    public override float Damage
    {
        get => _damage;
        set => _damage = value;
    }





}
