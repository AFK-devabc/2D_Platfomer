using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAniEvent : MonoBehaviour
{
    [SerializeField] Attacks attacks;
    public void Attack()
    {
        attacks.AniTriggerAttack();
    }
}
