using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill3 : AttackBase
{
    [SerializeField] private GameObject flyProjectile;
    [SerializeField] private Transform shootPoint01;
    [SerializeField] private Transform shootPoint02;
    int countState = 0;

    GameObject projectile1;
    GameObject projectile2;
    GameObject projectile3;
    GameObject projectile4;
    public override void ExcuteAttack(Transform targetPosi)
    {
        switch (countState)
        {
            case 0:
                projectile1 = Instantiate(flyProjectile, shootPoint01.position, Quaternion.identity);
                projectile1.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position+new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0,0));
                projectile2 = Instantiate(flyProjectile, shootPoint01.position, Quaternion.identity);
                projectile2.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));
                projectile3 = Instantiate(flyProjectile, shootPoint02.position, Quaternion.identity);
                projectile3.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));
                projectile4 = Instantiate(flyProjectile, shootPoint02.position, Quaternion.identity);
                projectile4.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));

                Debug.Log("AttackSkill03 0");
                countState++;
                break;
            case 1:
                projectile1 = Instantiate(flyProjectile, shootPoint01.position, Quaternion.identity);
                projectile1.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));
                projectile2 = Instantiate(flyProjectile, shootPoint01.position, Quaternion.identity);
                projectile2.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));
                projectile3 = Instantiate(flyProjectile, shootPoint02.position, Quaternion.identity);
                projectile3.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));
                projectile4 = Instantiate(flyProjectile, shootPoint02.position, Quaternion.identity);
                projectile4.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position + new Vector3(Random.Range(-GameConstant.RANGE_BOSS_SKILL03, GameConstant.RANGE_BOSS_SKILL03), 0, 0));

                Debug.Log("AttackSkill03 1");
                countState = 0;
                break;
        }
    }
}
