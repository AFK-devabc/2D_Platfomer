using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovement : MonoBehaviour, MovementBehavior
{

    [SerializeField] private Transform rayCastCheck;
    [SerializeField] private LayerMask mask;
    private bool isFacingRight = true;
    private bool isWalking = true;
    [SerializeField] private float raycastLength;
    [SerializeField] private float idleTime;
    [SerializeField] protected float movementSpeed;


    public void Move(Rigidbody2D rb, Animator ani)
    {
        rb.velocity = new Vector2(isFacingRight ? movementSpeed : -movementSpeed, rb.velocity.y);
        if (shouldFlip(rb))
            StartCoroutine(Flip(rb,ani));

    }

        private bool shouldFlip(Rigidbody2D rb)
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(rb.transform.position, -rb.transform.up, raycastLength, mask);
        Debug.DrawRay(rayCastCheck.position, -rb.transform.up * raycastLength, Color.red);

        RaycastHit2D wallInfo = Physics2D.Raycast(rayCastCheck.position, rb.transform.right, raycastLength, mask);
        Debug.DrawRay(rayCastCheck.position, rb.transform.right * raycastLength, Color.red);

        if (!groundInfo || wallInfo)
            return true;
        return false;
    }

    private IEnumerator Flip(Rigidbody2D rb, Animator ani)
    {
        rb.velocity = new Vector2(0, 5);
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
