using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class BossMeleeAttack : AttackBase
{
    const float SPEED_X_BOSS_MELEEATTACK = 2.0f;
    int countState = 0;

    public override void ExcuteAttack(Transform targetPosi)
    {
        //float distance = transform.position.x - targetPosi.position.x;
        //float speedMove = SPEED_X_BOSS_MELEEATTACK;
        //if (distance > 0)
        //    speedMove *= -1;

        switch (countState)
        {
            case 0:
                {
                    transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_MELEEATTACK;
                    Debug.Log("MeleeAttack01");
                    countState++;
                    break;
                }
            case 1:
                {
                    float distance = targetPosi.position.x - transform.position.x;
                    transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = distance*0.7f;
                    transform.parent.gameObject.GetComponentInParent<EnemyMovement>().Jump(12.0f);
                    Debug.Log("MeleeAttack02");
                    countState++;
                    break;
                }
            case 2:
                {
                    transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_COMBATMOVEMENT;
                    Debug.Log("MeleeAttack03");
                    countState = 0;
                    break;
                }
        };
    }
}
