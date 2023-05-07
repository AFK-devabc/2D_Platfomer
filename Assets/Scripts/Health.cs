using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float maxHealth;
    protected float health;

    private void Start()
    {
        health = maxHealth;
    }

    public virtual void TakeDamage(float damage, Transform hitPos = null)
    {
        health = health - damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    public float CurentHealth
    {
        get { return health; }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
