using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyEnemyMovement : EnemyMovement
{

    private bool canChangeVelocity = true;
    public override  void NormalMovement()
    {
        if(canChangeVelocity)
            StartCoroutine(GetRandomSpeed());
    }


    private IEnumerator GetRandomSpeed()
    {
        canChangeVelocity = false;
        rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * movementSpeed;
        Debug.Log(rb.velocity);

        yield return new WaitForSeconds(2.0f);
        canChangeVelocity = true;
    }

    public override void CombatMovement()
    {
        Vector2 dir = attackTarget.transform.position - transform.position;
        rb.velocity = dir.normalized * movementSpeed;
    }

}
