using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public abstract void Parry(float VelocityMult);

    public abstract bool IsParryable { get; set; }

    public abstract bool IsParried { get; set; }
}
