using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour
{
    public abstract void Parried();

    public abstract bool Parryable { get; set; }

    public abstract bool IsParried { get; set; }
}
