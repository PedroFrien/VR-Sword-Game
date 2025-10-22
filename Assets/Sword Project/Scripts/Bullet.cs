using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : BaseProjectile
{
    public bool _isParryable;
    public bool _isParried;

    public Rigidbody rb;

    private Vector3 previousPosition;

    [SerializeField] private float bulletRadius;
    [SerializeField] private LayerMask swordLayer;


    void Start()
    {
        previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        Vector3 direction = transform.position - previousPosition;
        float distance = direction.magnitude;

        if (Physics.SphereCast(previousPosition, bulletRadius, direction.normalized, out RaycastHit hit, distance, swordLayer))
        {
            Parry(2);
        }
    }

    public override void Parry(float VelocityMult)
    {
        Debug.Log("Bullet Got Parried");
        rb.velocity = rb.velocity * VelocityMult * -1;
        _isParried = true;
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
 

    
}
