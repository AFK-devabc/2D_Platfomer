using Assets.Scripts.GameConstant;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class EarthSlimeMovement : EnemyMovement
{
    [SerializeField] private Transform rayCastCheck;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float raycastLength;


    float timePrepareToWalk = 0.0f;

    bool checkIsOnPlatForm()
    {
        return isOnPlatForm;    
    }   

    private bool shouldFlip()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(rayCastCheck.position, rb.transform.right, raycastLength, mask);
        Debug.DrawRay(rayCastCheck.position, rb.transform.right * raycastLength, Color.red);

        if (checkIsOnPlatForm())
        {
            RaycastHit2D groundInfo = Physics2D.Raycast(rayCastCheck.position, -rb.transform.up, raycastLength, mask);
            Debug.DrawRay(rayCastCheck.position, -rb.transform.up * raycastLength, Color.red);

            if (wallInfo || !groundInfo)
                return true;
            return false;
        }

        if ( wallInfo)
            return true;
        return false;
    }

    public override void NormalMovement()
    {
        if (checkIsOnPlatForm())
        {
            if (timePrepareToWalk >= 0.4f)
            {
                MoveVertically(GameConstant.SPEED_Y_EARTHSLIME);
                timePrepareToWalk = 0.0f;
            }
            else
            {
                StopMovementX();    
                timePrepareToWalk += Time.deltaTime;
            }
        }
        else MoveHorizontally(movementSpeed);

        if (shouldFlip())
        {
            movementSpeed *= -1;
            Flipping();
        }

        //if(checkIsOnPlatForm())
        //{
        //    if (timePrepareToWalk >= 0.5f)
        //    { 
        //        rb.velocity = new Vector2(movementSpeed, SPEED_Y_EARTHSLIME);
        //        timePrepareToWalk = 0.0f;
        //    }
        //    else timePrepareToWalk += Time.deltaTime;
        //}
        //else
        //{
        //    if (deltaSpace <= 0)
        //    {
        //        Flipping();
        //        movementSpeed = -movementSpeed;
        //        deltaSpace = SPACE_AROUND_MOVEMENT;
        //    }
        //    rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        //}
    }

    protected virtual void Flipping()
    {
        if (movementSpeed < 0)
            rb.transform.rotation = Quaternion.Euler(0, -40, 0);
        else
            rb.transform.rotation = Quaternion.Euler(0, -120, 0);
    }

    public override void CombatMovement()
    {
        if (movementSpeed * (rb.transform.position.x - attackTarget.position.x) > 0)
        {
            movementSpeed *= -1;
        }

        if (checkIsOnPlatForm())
        {
            if (timePrepareToWalk >= 0.5f)
            {
                Jump(GameConstant.SPEED_Y_EARTHSLIME);
                timePrepareToWalk = 0.0f;
            }
            else {
                StopMovementX();
                timePrepareToWalk += Time.deltaTime; 
            }
        }
        else
        {
            Flipping();
            MoveHorizontally(movementSpeed);
        }

        //if (checkIsOnPlatForm())
        //{
        //    if (timePrepareToWalk >= 0.5f)
        //    {
        //        MoveHorizontally(movementSpeed);
        //        timePrepareToWalk = 0.0f;
        //    }
        //    else timePrepareToWalk += Time.deltaTime;
        //}
        //else
        //{
        //    //Debug.Log(rb.transform.position.x - attackTarget.position.x +";" + movementSpeed);
        //    if (movementSpeed*(rb.transform.position.x - attackTarget.position.x) > 0)
        //    {
        //        Flipping();
        //        movementSpeed *= -1;
        //    }   
        //    rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        //}

    }

    public override void SetTarget(Transform target)
    {
        attackTarget = target;
        movementSpeed = GameConstant.SPEED_X_EARTHSLIME_COMBAT;

        if (movementSpeed * (rb.transform.position.x - attackTarget.position.x) > 0)
        {
            movementSpeed *= -1;
            Flipping();
        }
    }
}
