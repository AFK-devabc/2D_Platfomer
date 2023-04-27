using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using static UnityEngine.GraphicsBuffer;

public class RockBugMovement : EnemyMovement
{
    [SerializeField] private Transform rayCastCheck;
    [SerializeField] private LayerMask mask;
    private bool isFacingRight = true;
    private bool isWalking = true;
    [SerializeField] private float raycastLength;
    [SerializeField] private float idleTime;

    [SerializeField] protected float movementSpeed;


    private float speedX = 0f;
    public override void NormalMovement()
    {
        if (!rayCastCheck)
            return;
        if (isWalking)
            MoveHorizontally(isFacingRight ? movementSpeed : -movementSpeed);
        if (shouldFlip())
            StartCoroutine(Flip());
    }

    public override void CombatMovement()
    {
        Vector2 dist = rb.transform.position - attackTarget.position;

        if (dist.x * rb.transform.right.x > 0)
        {
            isFacingRight = !isFacingRight;
            rb.transform.Rotate(0, 180f, 0);
        }
        if (dist.x < 5.0f)
        {
            speedX = isFacingRight ? -movementSpeed : movementSpeed;
            ani.SetBool("WalkBackward", true);
            ani.SetBool("Walking", false);
        }
        else if (dist.x > 15.0f)
        {
            speedX = isFacingRight ? movementSpeed : -movementSpeed;
            ani.SetBool("Walking", true);
            ani.SetBool("WalkBackward", false);
        }
        else if (dist.x > 7.5f && dist.x < 10.0f)
        {
            ani.SetBool("WalkBackward", false);
            ani.SetBool("Walking", false);
            speedX = 0;
        }
        MoveHorizontally(speedX);

    }

    private bool shouldFlip()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(rayCastCheck.position, -rb.transform.up, raycastLength, mask);
        Debug.DrawRay(rayCastCheck.position, -rb.transform.up * raycastLength, Color.red);

        RaycastHit2D wallInfo = Physics2D.Raycast(rayCastCheck.position, rb.transform.right, raycastLength, mask);
        Debug.DrawRay(rayCastCheck.position, rb.transform.right * raycastLength, Color.red);

        if (!groundInfo || wallInfo)
            return true;
        return false;
    }

    private IEnumerator Flip()
    {
        StopMovementX();
        ani.SetBool("Walking", false);
        isWalking = false;
        ani.SetTrigger("Jump");
        isFacingRight = !isFacingRight;

        rb.transform.Rotate(0, 180f, 0);

        yield return new WaitForSeconds(idleTime);
        ani.SetBool("Walking", true);
        isWalking = true;
    }
}
