using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossMovement : EnemyMovement
{
    const float SPEED_X_BOSS_COMBAT = 4.0f;
    
    float timeAppear = 0;
    bool isRight = false;
   
    public void Awake()
    {
        ani.SetTrigger("Sleep");
        velocity = new Vector2(0, 0);
        Debug.Log("Boss sleep");
    }

    public override void NormalMovement()
    {
        Debug.Log("normal movement");
    }

    public override void CombatMovement()
    {
        if (timeAppear > 2.0f)
        {
            if(isFollowPlayer)
            {
                if (Mathf.Abs(rb.transform.position.x - attackTarget.position.x) < collider2D.size.x)
                {
                    StopMovementX();
                    return;
                }

                if (rb.transform.position.x - attackTarget.position.x>0)
                {
                    isRight = false;
                }
                else isRight = true;

                if (movementSpeed * (rb.transform.position.x - attackTarget.position.x) > 0)
                {
                    movementSpeed *= -1;
                }
            }
            Flipping();
            MoveHorizontally(movementSpeed);
            
        }
        else timeAppear += Time.deltaTime;
    }

    protected  void Flipping()
    {
        if (!isRight)
            rb.transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            rb.transform.rotation = Quaternion.Euler(0, -180, 0);
    }

    public override void SetTarget(Transform target)
    {
        attackTarget = target;
        ani.SetTrigger("Appear");
        movementSpeed = SPEED_X_BOSS_COMBAT;
        timeAppear = 0.0f;
        isFollowPlayer = true;
        Debug.Log("appear");
    }
}
