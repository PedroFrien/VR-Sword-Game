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

    [SerializeField] private GameObject gunParticle;


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
            gunParticle.SetActive(true);


            float triggerValue = leftTriggerAction.action.ReadValue<float>();

            if (triggerValue > 0.5f) // Adjust threshold as needed
            {
                charging = true;
                FindObjectOfType<AudioManager>().PlayExclusiveSound("GunCharge", transform.position, gameObject);
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
        else
        {
            gunParticle.SetActive(false);
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

        FindObjectOfType<AudioManager>().PlaySound("PlayerShot", transform.position, gameObject);

        GameObject firedBullet = Instantiate(playerBullet, firePoint.position, firePoint.rotation);

        Vector3 aimDirection = laserSight.transform.forward;

        firedBullet.GetComponent<Rigidbody>().velocity = aimDirection * bulletSpeed;
        firedBullet.GetComponent<PlayerBullet>().speed = bulletSpeed;

    }

    private void ResetCharge()
    {
        currentCharge = 0;
        UpdateEnergyBar();
    }
}
