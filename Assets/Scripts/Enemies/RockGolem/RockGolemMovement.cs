using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolemMovement : EnemyMovement
{

    [SerializeField] private Transform rayCastCheck;
    [SerializeField] private LayerMask mask;
    private bool isFacingRight = true;
    [SerializeField] private float raycastLength;
    public override void NormalMovement()
    {
        if (!rayCastCheck)
            return;
        rb.velocity = new Vector2(isFacingRight ? movementSpeed: - movementSpeed, rb.velocity.y);

        if(shouldFlip())
           Flip();
    }

    public override void CombatMovement()
    {

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

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        rb.transform.Rotate(0f, 180f, 0f);
    }
}
