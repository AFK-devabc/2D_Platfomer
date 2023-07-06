using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSlimeAttack : AttackBase
{
    public override void ExcuteAttack(Transform targetPosi)
    {
                float distance = targetPosi.position.x - transform.position.x;
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().MoveHorizontally(distance * GameConstant.SPEED_X_EARTHSLIME_STTACK);
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().Jump(GameConstant.SPEED_Y_EARTHSLIME_ATTACK);
        
        Debug.Log("excute EarthSlime melee attack");
       
    }
}
