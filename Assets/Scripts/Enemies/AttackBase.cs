using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{

    [SerializeField] public bool hasAni = false;
    [SerializeField] public string aniTrigger = "";

    [SerializeField] public float attackCD;
    [SerializeField] public float attackDuration;
    [SerializeField] protected bool readyToAttack = true;
    [SerializeField] protected bool startWithCD;

    [SerializeField] protected bool haveRange;
    [SerializeField] protected float minRange;
    [SerializeField] protected float maxRange;

    [SerializeField] public bool stopMoving = true;

    public Color gizmosColor;

    public bool CanExcuteAttack(Vector3 targetPosi)
    {
        if (!readyToAttack) { return false; }  
        
        if (!haveRange) { return true; }

        float dis = Vector3.Distance(transform.position, targetPosi);

        if (IsInDistance(dis))
            return true;
        return false;
    }

    private bool IsInDistance(float dis)
    {
        if (dis < maxRange && dis > minRange)
            return true;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(transform.position, minRange);
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }

    public virtual void ExcuteAttack(Transform targetPosi)
    {
        if (attackCD > 0)
            StartCoroutine(AttackCD());
    }

    protected IEnumerator AttackCD()
    {
        readyToAttack = false;
        yield return new WaitForSeconds(attackCD);
        readyToAttack = true;
    }
}
