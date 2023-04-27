using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EarthSlimeMovement : EnemyMovement
{
    const float SPACE_AROUND_MOVEMENT = 300.0f;
    const float SPEED_Y_EARTHSLIME = 7.0f;
    const float SPEED_X_EARTHSLIME_WALK = 1.0f;
    const float SPEED_X_EARTHSLIME_COMBAT = 2.0f;
    float deltaSpace = SPACE_AROUND_MOVEMENT;
    [SerializeField] protected float movementSpeed;

    float timePrepareToWalk = 0.0f;

    bool checkIsOnPlatForm()
    {
        if (rb.transform.position.y < -3.5)
            return true;
        return false;
    }

    public override void NormalMovement()
    {
        if(checkIsOnPlatForm())
        {
            if (timePrepareToWalk >= 0.5f)
            { 
                rb.velocity = new Vector2(movementSpeed, SPEED_Y_EARTHSLIME);
                timePrepareToWalk = 0.0f;
            }
            else timePrepareToWalk += Time.deltaTime;
        }
        else
        {
            if (deltaSpace <= 0)
            {
                Flipping();
                movementSpeed = -movementSpeed;
                deltaSpace = SPACE_AROUND_MOVEMENT;
            }
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }

        deltaSpace -= Math.Abs(movementSpeed);
    }

    protected virtual void Flipping()
    {
        if (movementSpeed < 0)
            rb.transform.rotation = Quaternion.Euler(0, -120, 0);
        else
            rb.transform.rotation = Quaternion.Euler(0, -40, 0);
    }

    public override void CombatMovement()
    {
        
        if (checkIsOnPlatForm())
        {
            if (timePrepareToWalk >= 0.5f)
            {
                rb.velocity = new Vector2(movementSpeed, SPEED_Y_EARTHSLIME);
                timePrepareToWalk = 0.0f;
            }
            else timePrepareToWalk += Time.deltaTime;
        }
        else
        {
            //Debug.Log(rb.transform.position.x - attackTarget.position.x +";" + movementSpeed);
            if (movementSpeed*(rb.transform.position.x - attackTarget.position.x) > 0)
            {
                Flipping();
                movementSpeed *= -1;
            }   
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }

    }

    public override void SetTarget(Transform target)
    {
        attackTarget = target;
        movementSpeed = SPEED_X_EARTHSLIME_COMBAT;
    }
}
