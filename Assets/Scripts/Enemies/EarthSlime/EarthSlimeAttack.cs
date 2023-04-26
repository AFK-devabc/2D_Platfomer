using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSlimeAttack : AttackBase
{
    const float SPEED_Y_EARTHSLIME_ATTACK = 15.0f;
    const float SPEED_X_EARTHSLIME_STTACK = 1.0f;

    public override void ExcuteAttack(Transform targetPosi)
    {
        float distance = targetPosi.position.x - transform.position.x;
        Debug.Log("excute EarthSlime melee attack");
        transform.parent.gameObject.GetComponentInParent<EnemyMovement>().SetVelocity(new Vector2(distance*SPEED_X_EARTHSLIME_STTACK, SPEED_Y_EARTHSLIME_ATTACK));
        base.ExcuteAttack(targetPosi);
    }
}
