using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] protected HealthbarBehavior healthbar;

    private void Start()
    {
        health = maxHealth;
        healthbar.SetHealth(health, maxHealth);
    }

    public override void TakeDamage(float damage, Transform hitPos = null)
    {
        health = health - damage;
        healthbar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            Die();
        }
    }

}
