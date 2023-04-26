using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] protected float movementSpeed;
    protected Transform attackTarget;

    protected Rigidbody2D rb;
    protected Animator ani;

    [SerializeField] protected bool canMove;

    [SerializeField] protected bool stopped;
    [SerializeField] protected bool noFlipping;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2 (0, 0);
        ani=GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if(stopped) 
            return;
        if(canMove)
        {
            if(!attackTarget)
            {
                NormalMovement();
            }
            else
            {
                CombatMovement();
            }
        }
    }

    public virtual void NormalMovement()
    {

    }

    public virtual void CombatMovement()
    {
        if(!noFlipping)
            Flipping();
    }

    protected virtual void Flipping()
    {
        if(attackTarget)
        {
            float dir = attackTarget.position.x- transform.position.x;

            if (dir > 0)
                rb.transform.rotation = Quaternion.Euler(0, 45, 0);
            else
                rb.transform.rotation = Quaternion.Euler(0, -45, 0);
        }
    }

    public virtual void SetTarget(Transform target)
    {
        attackTarget = target;
    }

    public void StopMoving(float  time)
    {
        stopped = true;
        rb.velocity = new Vector2 (0, 0);
        StartCoroutine(StopCount(time));
    }
    
    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }


    public IEnumerator StopCount(float time)
    {
        yield return new WaitForSeconds(time);
        stopped = false;
    }

}
