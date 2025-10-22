using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseCharacter
{

    public float _health;
    public override void TakeDamage(float damageTaken)
    {
        _health -= damageTaken;

        Debug.Log("Player is at " + _health + "health");

        if (_health <= 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        Debug.Log("Player Died");

        Destroy(gameObject);
    }

    public override float Health
    {
        get => _health;
        set => _health = value;
    }
}
