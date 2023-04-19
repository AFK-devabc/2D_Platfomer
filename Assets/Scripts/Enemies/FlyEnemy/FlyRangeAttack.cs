using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRangeAttack : AttackBase
{
    [SerializeField] private EnemyMovement movBeh;
    [SerializeField] private GameObject flyProjectile;
    [SerializeField] private Transform shootPoint;


    public override void ExcuteAttack(Transform targetPosi)
    {

        GameObject projectile = Instantiate(flyProjectile, shootPoint.position, Quaternion.identity);

        projectile.GetComponent<ProjectileMovement>().SetTarget(targetPosi.position);

        base.ExcuteAttack(targetPosi);
    }
}
