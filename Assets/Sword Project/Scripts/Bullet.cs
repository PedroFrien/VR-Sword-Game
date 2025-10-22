using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseProjectile
{
    private bool _parryable;
    private bool _isParried;

    public Rigidbody rb;



    public override void Parried()
    {
        rb.velocity = rb.velocity * -1;
    }

    public override bool Parryable 
    {
        get { return _parryable; }
        set
        {
            _parryable = value;

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
