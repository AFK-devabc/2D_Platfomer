using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRangeAttack : AttackBase
{
    [SerializeField] private EnemyMovement movBeh;

    public override void ExcuteAttack(Transform targetPosi)
    {
        readyToAttack = false;
        movBeh.StopMoving(1.0f);
        if (attackCD > 0)
        {
            StartCoroutine(AttackCD());
        }
    }


}
