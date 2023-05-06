using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Animator playerAnimator;
    [SerializeField] private LayerMask platformLayer;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;

    [Header("----------InputSystem----------")]
    private PlayerInputActions playerInputActions;
    private InputAction move;
    private InputAction meleeAttack;
    private InputAction jump;
    private InputAction dash;

    [Header("----------NormalMovement----------")]
    private bool isFacingRight;

    private float horizontal;
    [SerializeField] private float movementSpeed;

    [SerializeField] private float jumpForce;
    [SerializeField] private float extraHeight;
    private bool isGrounded;

    [Header("----------Dashing----------")]
    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;

    private bool isDashing = false;
    private bool canDash = true;
    [Header("----------MeleeAttack----------")]
    [SerializeField] private float meleeAttackTime;
    private bool isAttacking = false;
    private bool canAttack = true;
    [SerializeField] private GameObject swordSlash;

    private void Awake()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerInputActions= new PlayerInputActions();
        isFacingRight = true ;
    }

    private void OnEnable()
    {
        move = playerInputActions.Player.Move;
        move.Enable();

        meleeAttack = playerInputActions.Player.MeleeAttack;
        meleeAttack.Enable();
        meleeAttack.performed += MeleeAttack;
   
        jump= playerInputActions.Player.Jump;   
        jump.Enable();
        jump.performed += Jump;
        jump.canceled += CancelJump;

        dash = playerInputActions.Player.Dashing;
        dash.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
        meleeAttack.Disable();
        jump.Disable();
        dash.Disable();
    }
    private void Update()
    {
        if (isDashing || isAttacking)
            return;

        isGrounded = IsGrounded();
        if(isGrounded) { canDash = true; }
        horizontal = move.ReadValue<Vector2>().x;

        if(horizontal != 0f ) 
        { 
            if (horizontal < 0 && isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (horizontal > 0 && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (dash.triggered && canDash && !isAttacking)
        {
            StartCoroutine(Dash());
        }
        SetAni();
    }

    private void SetAni()
    {
        playerAnimator.SetBool("IsGrounded", isGrounded);

        playerAnimator.SetBool("IsRunning", horizontal != 0);

        playerAnimator.SetBool("IsDropping", rb.velocity.y < 0);

    }

    private void FixedUpdate()
    {
        if(isDashing || isAttacking)
        {
            return;
        }
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    private void MeleeAttack(InputAction.CallbackContext context)
    {
        if (isAttacking || isDashing)
            return;
        GameObject projectile = Instantiate(swordSlash, this.transform.position, Quaternion.identity);

        playerAnimator.SetTrigger("MeleeAttackTrigger");
        StartCoroutine(AttackCountdown());   
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    private void CancelJump(InputAction.CallbackContext context)
    {
        if (isGrounded && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.5f);
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        playerAnimator.SetTrigger("DashTrigger");
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashingVelocity * (isFacingRight ? 1 : -1), 0f);
        yield return new WaitForSeconds(dashingTime);
 
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extraHeight, platformLayer);
        return raycastHit.collider != null;
    }

    private IEnumerator AttackCountdown()
    {
        isAttacking = true;
        yield return new WaitForSeconds(meleeAttackTime);
        isAttacking = false;
    }

}
