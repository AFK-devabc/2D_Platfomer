using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{

    [SerializeField] public bool hasAni = false;
    [SerializeField] public string aniTrigger = "";

    [SerializeField] protected float attackCD;
    [SerializeField] protected float targetDuration;
    [SerializeField] protected bool readyToAttack = true;
    [SerializeField] protected bool startWithCD;

    [SerializeField] protected bool haveRange;
    [SerializeField] protected float minRange;
    [SerializeField] protected float maxRange;

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
        if (dis < maxRange && maxRange > minRange)
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
    }

    protected IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCD);

        readyToAttack = true;

    }
}
