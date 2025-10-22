using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    public bool _isParryable;
    public bool _isParried;

    public Rigidbody rb;



    public override void Parry(float VelocityMult)
    {
        Debug.Log("Bullet Got Parried");
        rb.velocity = rb.velocity * VelocityMult * -1;
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
