using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    [SerializeField] private List<AttackBase> attacks;

     private Animator animator;
    private EnemyMovement movbeh;

    private Transform attackTarget;
    private AttackBase currentAttack;

    private bool isAttacking;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movbeh = GetComponent<EnemyMovement>();
        isAttacking = false;
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

        currentAttack = attack;

       StartCoroutine(  WaitForAttack(currentAttack.attackDuration));

        if(!currentAttack.hasAni)
            currentAttack.ExcuteAttack(attackTarget);

        animator.SetTrigger(currentAttack.aniTrigger);

        Debug.Log(currentAttack.aniTrigger);
        movbeh.StopMoving(currentAttack.attackDuration);
    }

    public void AniTriggerAttack()
    {
        currentAttack.ExcuteAttack(attackTarget);
    }

    private void Update()
    {
        if(!attackTarget)
            return;

        if (isAttacking)
            return;

        Attack();
    }

    public void SetTarget(Transform target)
    {
        attackTarget = target;
    }

    private IEnumerator WaitForAttack(float delay)
    {
        isAttacking = true;
       yield return new WaitForSeconds(delay);

        isAttacking = false;
    }

}
