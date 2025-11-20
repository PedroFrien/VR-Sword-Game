using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : BaseCharacter
{

    public float _health;

    public float maxHealth;

    [SerializeField] private TMP_Text healthCounter;

    [SerializeField] private string[] hurtSounds;
    public override void TakeDamage(float damageTaken)

    {
        _health -= damageTaken;

        Debug.Log("Player is at " + _health + "health");

        healthCounter.text = _health.ToString();

        int randomIndex = Random.Range(0, hurtSounds.Length);
        string hurtSound = hurtSounds[randomIndex];
        FindObjectOfType<AudioManager>().PlaySound(hurtSound, transform.position, gameObject);



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

    public void Heal(float amountHealed)
    {
        _health += amountHealed;

        _health = Mathf.Min(_health, maxHealth);

        healthCounter.text = _health.ToString();

    }

    private void Start()
    {
        _health = maxHealth;

        if (healthCounter != null)
        {
            healthCounter.text = _health.ToString();
        }
    }

    public void ResetHealth()
    {
        _health = maxHealth;
        healthCounter.text = _health.ToString();
    }
}
