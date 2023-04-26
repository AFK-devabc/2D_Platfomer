using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossMeleeAttack : AttackBase
{
    const float SPEED_X_BOSS_MELEEATTACK = 2.0f;

    public override void ExcuteAttack(Transform targetPosi)
    {
        float distance = transform.position.x - targetPosi.position.x;
        float speedMove = SPEED_X_BOSS_MELEEATTACK;
        if (distance > 0)
            speedMove *= -1;
        Debug.Log("BossMeleeAttack");
        //Vector2 velocity = transform.parent.gameObject.GetComponentInParent<EnemyMovement>().GetVelocity();
        //transform.parent.gameObject.GetComponentInParent<EnemyMovement>().SetVelocity(new Vector2(speedMove, velocity.y));
    }
}
