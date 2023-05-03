using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill2 : AttackBase
{
    int countState = 0;
    public override void ExcuteAttack(Transform targetPosi)
    {
        float distance = transform.position.x - targetPosi.position.x;
        int i = distance > 0 ? -1 : 1;
        switch (countState)
        {
            case 0:
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_COMBATMOVEMENT * i;
                countState++;
                Debug.Log("attackSkill02 0");
                break;
            case 1:
                transform.parent.parent.GetChild(0).GetComponent<Animator>().SetTrigger("StopTrigger02");
                countState = 0;
                Debug.Log("attackSkill02 1");
                break;
        }
    }
}
