using Assets.Scripts.GameConstant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSkill4 : AttackBase
{
    [SerializeField] private GameObject flyProjectile;
    [SerializeField] private Transform shootPoint;

    int countState = 0;
    public override void ExcuteAttack(Transform targetPosi)
    {
        float distance = 0f;
        switch (countState)
        {
            case 0:
                for (int i = 0; i < 6; i++)
                {
                    GameObject projectile1 = Instantiate(flyProjectile, shootPoint.position, Quaternion.identity);
                    projectile1.GetComponent<ProjectileMovement>().SetTarget(transform.position + new Vector3(distance + 8.0f, 0, 0));
                    GameObject projectile2 = Instantiate(flyProjectile, shootPoint.position, Quaternion.identity);
                    projectile2.GetComponent<ProjectileMovement>().SetTarget(transform.position + new Vector3(-distance - 8.0f, 0, 0));

                    distance += 4.0f;
                }
                countState++;
                Debug.Log("AttackSkill04 0");
                break;
            case 1:
                distance = 4.0f;
                for (int i = 0; i < 6; i++)
                {
                    GameObject projectile1 = Instantiate(flyProjectile, shootPoint.position, Quaternion.identity);
                    projectile1.GetComponent<ProjectileMovement>().SetTarget(transform.position + new Vector3(distance + 8.0f, 0, 0));
                    GameObject projectile2 = Instantiate(flyProjectile, shootPoint.position, Quaternion.identity);
                    projectile2.GetComponent<ProjectileMovement>().SetTarget(transform.position + new Vector3(-distance - 8.0f, 0, 0));

                    distance += 4.0f;
                }
                countState = 0;
                Debug.Log("AttackSkill04 1");
                break;
        }
        
        
    }
}
