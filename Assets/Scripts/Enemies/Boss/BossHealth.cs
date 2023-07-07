using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    [SerializeField] private PausedMenu pauseMenu;

    protected override void Die()
    {
        if (deadEffect != null)
        {
            Instantiate(deadEffect, transform.position, Quaternion.identity);
        }
        pauseMenu.GameEnded();
        gameObject.SetActive(false);
        isDestroyed = true;
    }

}
