using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSlimeAttack : AttackBase
{
    int countState = 0;
    public override void ExcuteAttack(Transform targetPosi)
    {
        switch (countState) { 
            case 0:
                float distance = targetPosi.position.x - transform.position.x;
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = distance * GameConstant.SPEED_X_EARTHSLIME_STTACK;
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().Jump(GameConstant.SPEED_Y_EARTHSLIME_ATTACK);
                countState++;
                break;
            case 1:
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_COMBATMOVEMENT;
                countState = 0;
                break;
        }
        
        Debug.Log("excute EarthSlime melee attack");
       
    }
}
