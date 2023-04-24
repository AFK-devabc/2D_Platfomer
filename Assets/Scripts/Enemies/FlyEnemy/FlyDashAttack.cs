using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyDashAttack : AttackBase
{
    [SerializeField] private float movSpeed;

    public override void ExcuteAttack(Transform targetPosi)
    {
        Vector2 velocity = targetPosi.position - transform.position;

        transform.parent.gameObject.GetComponentInParent<EnemyMovement>().SetVelocity(velocity.normalized * movSpeed);

        base.ExcuteAttack(targetPosi);
    }

}
