using System;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public Transform attackContainer;
    [SerializeField] private List<AttackBase> attacks;

    [SerializeField] private Animator animator;

    private Transform attackTarget;

    private void Awake()
    {
        attackContainer = GetComponent<Transform>();
    }

    private AttackBase AttackToUse()
    {
        List<AttackBase> availableAttacks = new List<AttackBase>();

        foreach (AttackBase items in attacks)
        {
            if (items.CanExcuteAttack(attackTarget.position))
                availableAttacks.Add(items);
        }

        if (availableAttacks.Count > 0)
        {
            int i = UnityEngine.Random.Range(0, availableAttacks.Count);
            return availableAttacks[i];
        }
        else
            return null;
    }

    void Attack()
    {
        AttackBase attack = AttackToUse();
        if (!attack)
            return;

        if(!attack.hasAni) 
            attack.ExcuteAttack(attackTarget);

        animator.SetTrigger(attack.aniTrigger);
    }

    private void Update()
    {
        if(!attackTarget)
            return;

        Attack();
    }

    public void SetTarget(Transform target)
    {
        attackTarget = target;
    }
}
