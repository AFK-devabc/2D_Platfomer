using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBase : MonoBehaviour
{

    protected string aniTrigger = "";

    protected float attackCD;
    protected float targetDuration;
    protected bool readyToAttack = true;
    protected bool startWithCD;

    protected bool noMaxRange;
    protected float minRange;
    protected float maxRange;

    public bool showGizmos;
    public Color gizmosColor;

    public bool CanExcuteAttack(Vector3 playerPosi)
    {
        if (!readyToAttack) { return false; }  
        
        if (!noMaxRange) { return true; }

        float dis = Vector3.Distance(transform.position, playerPosi);

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
        if( !showGizmos )
            return;
        Gizmos.color = gizmosColor;
        Gizmos.DrawSphere(transform.position, minRange);
        Gizmos.DrawSphere(transform.position, maxRange);
    }

    public virtual void ExcuteAttack(Transform player)
    {
        readyToAttack = false;
        if(attackCD > 0 ) { 
            StartCoroutine(AttackCD()); 
        }
    }

    private IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCD);

        readyToAttack = true;

    }



}
