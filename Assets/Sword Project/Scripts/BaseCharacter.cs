using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    public abstract void TakeDamage(float damageTaken);

    public abstract void Die();

    public abstract float Health { get; set; }
}
