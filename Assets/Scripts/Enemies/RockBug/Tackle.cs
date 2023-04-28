using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tackle : AttackBase
{
    // Start is called before the first frame update
    [SerializeField] private float movSpeed;



    public override void ExcuteAttack(Transform targetPosi)
    {
             transform.parent.gameObject.GetComponentInParent<EnemyMovement>().SetVelocity(new Vector2(1, 0).normalized * movSpeed);
        attackState++;
    } 
}
