using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private LineRenderer laserSight;

    [SerializeField] private Transform firePoint;

    [SerializeField] private float bulletSpeed;

    public InputActionReference leftTriggerAction;


    private bool charging = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = leftTriggerAction.action.ReadValue<float>();

        if (triggerValue > 0.5f) // Adjust threshold as needed
        {
            charging = true;
        }

        if (triggerValue < 0.5f)
        {
            charging = false;
        }


        if (charging)
        {
            laserSight.enabled = true;
        }
        else
        {
            laserSight.enabled = false;
        }
    }

    void OnEnable()
    {
        if (leftTriggerAction != null)
        {
            leftTriggerAction.action.Enable();
        }
    }
}
