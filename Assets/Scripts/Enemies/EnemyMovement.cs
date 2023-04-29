using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public abstract class EnemyMovement : MonoBehaviour
{
    protected Transform attackTarget;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected BoxCollider2D collider2D;
    [SerializeField] protected LayerMask groundLayer;

    [SerializeField] protected Animator ani;

    protected Vector2 velocity;
    [SerializeField] private float safetyDistance = 0.005f;

    [SerializeField] protected bool canMove;

    [SerializeField] protected float gravity;
    [SerializeField] protected bool noFlipping;

    [SerializeField] public float movementSpeed;

    protected bool isOnPlatForm = false;
    protected bool isJumping = false;
    public bool isFollowPlayer = false;

    private void Start()
    {
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
        isOnPlatForm = false;
    }
    public void PerformMovement()
    {
        //Debug.Log(velocity);
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

    private void HandleCollisionMovement()
    {
        CheckCollisionForHorizontalMovement();
        CheckCollisionForVerticallyMovement();
    }

    private void CheckCollisionForVerticallyMovement()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(collider2D.transform.position, collider2D.transform.up * (velocity.y > 0? 1:-1), collider2D.size.y/2, groundLayer);
        if(groundInfo)
        {
            float sign = Mathf.Sign(groundInfo.point.y - transform.position.y);
            float distance = groundInfo.distance - safetyDistance;
            ForceMovement(new Vector2(0, sign * distance));
            StopMovementY();
        }
    }
    private void CheckCollisionForHorizontalMovement()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(collider2D.transform.position, collider2D.transform.right * (velocity.x > 0? 1:-1), collider2D.size.x/2, groundLayer);
        if(groundInfo)
        {
            float sign = Mathf.Sign(groundInfo.point.x - transform.position.x);
            float distance = groundInfo.distance - safetyDistance;
            ForceMovement(new Vector2(sign * distance, 0));
            StopMovementX();
        }
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

    public void Jump(float vy)
    {
        isJumping = true;
        velocity.y = vy;    
    }

    public void ApplyGravity()
    {
        velocity += Physics2D.gravity * gravity * Time.fixedDeltaTime;
        if (isJumping)
        {
            isJumping = false;
            return;
        }
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        if(raycastHit.collider)
        {
            velocity.y = 0;
            isOnPlatForm = true;
        }
        //velocity.y -= gravity * Time.fixedDeltaTime;
    }
    public void StopMovementX() => velocity.Set(0, velocity.y) ;

    public void StopMovementY() => velocity.Set(velocity.x, 0);

    public void StopMovementBothAxis() => velocity.Set(0, 0);

    public void setIsFlowPlayer(bool isFlowPlayer) { this.isFollowPlayer = isFlowPlayer; }
}
