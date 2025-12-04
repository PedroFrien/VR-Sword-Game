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

    [SerializeField] private HealthbarImage energyBar;

    [SerializeField] private GameObject playerBullet;

    [SerializeField] private float maxCharge = 3;
    [SerializeField] private float currentCharge;


    private bool charging = false;


    // Start is called before the first frame update
    void Start()
    {
        UpdateEnergyBar();
    }

    public void AddEnergy(int amount)
    {
        currentCharge += amount;

        UpdateEnergyBar();

        Debug.Log(currentCharge);
    }

    private void UpdateEnergyBar()
    {
        float value = currentCharge / maxCharge;

        Debug.Log(value);

        energyBar.ChangeValue(value);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentCharge >= maxCharge)
        {
            float triggerValue = leftTriggerAction.action.ReadValue<float>();

            if (triggerValue > 0.5f) // Adjust threshold as needed
            {
                charging = true;
            }

            if (triggerValue < 0.5f)
            {
                if (charging)
                {
                    Fire();
                }


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
        
    }

    void OnEnable()
    {
        if (leftTriggerAction != null)
        {
            leftTriggerAction.action.Enable();
        }
    }

    private void Fire()
    {
        ResetCharge();

        GameObject firedBullet = Instantiate(playerBullet, firePoint.position, firePoint.rotation);

        Vector3 aimDirection = laserSight.transform.forward;

        firedBullet.GetComponent<Rigidbody>().velocity = aimDirection * bulletSpeed * Time.deltaTime;
        firedBullet.GetComponent<PlayerBullet>().speed = bulletSpeed;

    }

    private void ResetCharge()
    {
        currentCharge = 0;
        UpdateEnergyBar();
    }
}
