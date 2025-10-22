using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBlade : MonoBehaviour
{
    [SerializeField] private float velocityMult = 1.1f;


    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        BaseProjectile projectile = other.gameObject.GetComponent<BaseProjectile>();
        if (projectile != null && projectile.IsParryable == true && projectile.IsParried == false)
        {
            projectile.Parry(velocityMult);
        }
    }

  
}
