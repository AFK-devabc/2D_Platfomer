using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    private List<AttackBase> attacks;

    private Animator animator;
    private EnemyMovement movbeh;

    private Transform attackTarget;
    private AttackBase currentAttack;

    private bool isAttacking;

    [SerializeField] private float timeAppear = 0.0f;
    private float timeAppearStart = 0.0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(!animator)
        {
            animator = transform.GetChild( 0 ).GetComponent<Animator>();
        }
        movbeh = GetComponent<EnemyMovement>();
        isAttacking = false;
        GetListAttacks();
    }

    public void GetListAttacks()
    {
        AttackBase[] listAttacks = gameObject.GetComponentsInChildren<AttackBase>();
        attacks = new List<AttackBase>();
        attacks.Clear();
        for (int i = 0; i < listAttacks.Length; i++)
            attacks.Add(listAttacks[i]);    

    }

    private AttackBase AttackToUse()
    {
        if(timeAppearStart < timeAppear)
        {
            timeAppearStart += Time.deltaTime;
            return null;
        }    
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
        StartCoroutine(WaitForAttack(currentAttack.attackDuration));

        if(attack.stopMoving)
            movbeh.StopMovementBehavior(currentAttack.attackDuration);

        if (!currentAttack.hasAni)
            currentAttack.ExcuteAttack(attackTarget);

        animator.SetTrigger(currentAttack.aniTrigger);
        StartCoroutine(currentAttack.AttackCD());
        Debug.Log(currentAttack.aniTrigger);
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

    public virtual void SetTarget(Transform target)
    {
        attackTarget = target;
    }

    private IEnumerator WaitForAttack(float delay)
    {
        isAttacking = true;
        yield return new WaitForSeconds(delay);
        if (currentAttack.isHasEventFinishAttack)
        {
            currentAttack.ExcuteAttack(attackTarget);
        }
        isAttacking = false;
    }
}
