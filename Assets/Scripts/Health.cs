using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;

    [SerializeField] private GameObject deadEffect;
    [SerializeField] private GameObject hitEffect;

    public void TakeDamage(float damage)
    {
        health = health - damage;
        hitEffect.SetActive(true);
        if(health < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        deadEffect.SetActive(true);
    }
}
