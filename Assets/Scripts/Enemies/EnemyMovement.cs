using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyMovement : MonoBehaviour
{
    protected Transform attackTarget;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D collider2D;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] protected Animator ani;

    protected Vector2 velocity;
    [SerializeField] private float safetyDistance = 0.005f;

    [SerializeField] protected bool canMove;

    [SerializeField] protected float gravity;
    [SerializeField] protected bool noFlipping;


    private void Start()
    {
        //rb.isKinematic = true;
        velocity = new Vector2(0, 0);
    }

    private void FixedUpdate()
    {
        ApplyGravity();
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

        PerformMovement();
    }
    public void PerformMovement()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }

    public abstract void NormalMovement();

    public abstract void CombatMovement();

    public virtual void SetTarget(Transform target)
    {
        attackTarget = target;
    }

    public void StopMovementBehavior(float  time)
    {
        StopMovementBothAxis();
        canMove = false;
        StartCoroutine(StopCount(time));
    }

    private IEnumerator StopCount(float time)
    {
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }

    public Vector2 GetVelocity()
    {
        return velocity;
    }
    public void ForceMovement(Vector2 offset)
    {
        rb.position = rb.position + offset;
    }

    public void MoveHorizontally(float vX)
    {
        velocity.x = vX;
    }

    public void MoveVertically(float vY)
    {
        velocity.y = vY;
    }

    public void ApplyGravity()
    {
        velocity.y += gravity * Time.fixedDeltaTime;
    }
    public void StopMovementX() => velocity.Set(0, velocity.y) ;

    public void StopMovementY() => velocity.Set(velocity.x, 0);

    public void StopMovementBothAxis() => velocity.Set(0, 0);
}
