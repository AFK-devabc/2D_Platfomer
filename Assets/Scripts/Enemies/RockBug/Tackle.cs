using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tackle : AttackBase
{
    // Start is called before the first frame update
    [SerializeField] private float movSpeed;
    [SerializeField] private EnemyMovement eneMov;



    public override void ExcuteAttack(Transform targetPosi)
    {
        eneMov.MoveHorizontally((targetPosi.position.x - transform.position.x) > 0? movSpeed : -movSpeed);
    } 
}
