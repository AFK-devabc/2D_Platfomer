using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill1 : AttackBase
{
    int countState = 0;
    public override void ExcuteAttack(Transform targetPosi)
    {
        float distance = transform.position.x - targetPosi.position.x; 
        int i = distance > 0 ? -1 : 1;
        switch (countState)
        {
            case 0:
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_ATTACKSKILL01*i;
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().setIsFlowPlayer(false);
                countState++;
                Debug.Log("attackSkill01 0");
                break;
            case 1:
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().movementSpeed = GameConstant.SPEED_X_BOSS_COMBATMOVEMENT*i;
                transform.parent.gameObject.GetComponentInParent<EnemyMovement>().isFollowPlayer = true;
                transform.parent.parent.GetChild(0).GetComponent<Animator>().SetTrigger("StopTrigger01");
                //transform.parent.gameObject.GetComponentInParent<Animator>().SetTrigger("StopTrigger01");    
                countState = 0;
                Debug.Log("attackSkill01 1");
                break;
        }
    }

}
