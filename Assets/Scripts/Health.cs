using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected EnemyStarts enemyStarts;
    protected float health;

    [SerializeField] protected GameObject deadEffect;

    protected bool isDestroyed = false;

    private void Start()
    {
        health = enemyStarts.maxHealth;
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
        get { return enemyStarts.maxHealth; }
    }

    public float CurentHealth
    {
        get { return health; }
    }

    protected virtual void Die()
    {
        if (deadEffect != null)
        {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
        }
        gameObject.SetActive(false);
        isDestroyed = true;
    }


}
