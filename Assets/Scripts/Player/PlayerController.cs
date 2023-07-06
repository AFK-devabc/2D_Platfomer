using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IDataPersistence
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

    [Header("----------Jump----------")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float extraHeight;
     private int extraJump = 0;
    private int jumpCount;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;

    private bool isGrounded;

    [Header("----------Dashing----------")]

    private bool canUseDash = false;
    [SerializeField] private float dashingVelocity;
    [SerializeField] private float dashingTime;

    private bool isDashing = false;
    private bool canDash = true;

    [Header("----------Attack----------")]
    [SerializeField] private float meleeAttackTime;
    private bool isAttacking = false;
    [SerializeField] private GameObject swordSlash;
    [SerializeField] private GameObject playerProjectile;

    [SerializeField] private float attackPoint;
    [SerializeField] private Vector2 attackSize;
    private bool isStopControl = false;

    private float projectSpeed = 50.0f;

    [Header("----------Controll camera follow object----------")]
    [SerializeField] private CameraFollowController cameraFollowController;
    private float fallSpeedYDampingChangeThreshold;

    private void Awake()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        playerInputActions= new PlayerInputActions();

        move = playerInputActions.Player.Move;

        meleeAttack = playerInputActions.Player.MeleeAttack;
        meleeAttack.performed += RangeAttack;

        jump = playerInputActions.Player.Jump;
        jump.performed += Jump;
        jump.canceled += CancelJump;

        dash = playerInputActions.Player.Dashing;
        dash.performed += Dash;


    }

    private void Start()
    {
        isFacingRight = true;

        fallSpeedYDampingChangeThreshold = CameraManager.instance.fallSpeedYDampingThreshold;

    }

    private void OnEnable()
    {
        EnableInput();

    }
    private void OnDisable()
    {
        DisableInput();
    }

    private void DisableInput()
    {
        move.Disable();
        meleeAttack.Disable();
        jump.Disable();
        dash.Disable();
    }
    private void EnableInput()
    {
        move.Enable();
        meleeAttack.Enable();
        jump.Enable();
        dash.Enable();
    }


    public void LoadData(GameData data)
    {
        this.transform.position = data.playerPosition;
    }

    public void SaveData( GameData data)
    {
        data.playerPosition = this.transform.position;
    }
    public void ReloadData(GameData data)
    {
        this.transform.position = data.playerPosition;
  
    }
    private void Update()
    {
        jumpBufferCounter -= Time.deltaTime;

        if (rb.velocity.y < fallSpeedYDampingChangeThreshold && !CameraManager.instance.IsLerpingYDamping && !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }
        if (rb.velocity.y >= 0f  && !CameraManager.instance.IsLerpingYDamping && CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpedFromPlayerFalling = false;
            CameraManager.instance.LerpYDamping(false);
        }

        if(isDashing)
        {
            return;
        }

        if(isAttacking)
        {
            return;
        }

        isGrounded = IsGrounded();
        if(isGrounded)
        { 
            canDash = true;
            jumpCount = 0;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(jumpBufferCounter > 0 && coyoteTimeCounter > 0 )
            PerformJump();
        else if(jumpBufferCounter > 0 && jumpCount < extraJump)
        {
            jumpCount++;
            Debug.Log(jumpCount);
            PerformJump();
        }
        horizontal = move.ReadValue<Vector2>().x;

        if(horizontal != 0f ) 
        { 
            if (horizontal < 0 && isFacingRight)
            {
                isFacingRight = !isFacingRight;
                rb.transform.Rotate(0, 180f, 0);
                cameraFollowController.CallTurn();
            }
            else if (horizontal > 0 && !isFacingRight)
            {
                isFacingRight = !isFacingRight;
                rb.transform.Rotate(0, 180f, 0);
                cameraFollowController.CallTurn();
            }
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
        if(isDashing || isAttacking || isStopControl)
        {
            return;
        }
       
        rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
    }

    private void RangeAttack(InputAction.CallbackContext context)
    {
        if (isAttacking || isDashing)
            return;
        
        if (move.ReadValue<Vector2>().y > 0)
        {
            GameObject projectile = Instantiate(playerProjectile, 
                                                new Vector3(transform.position.x, transform.position.y + attackPoint, transform.position.z), 
                                                Quaternion.identity);
            projectile.GetComponent<PlayerProjectileMovement>().SetVelocity(new Vector2(0, projectSpeed));
        }
        else if(move.ReadValue<Vector2>().y < 0 && !isGrounded)
        {
            GameObject projectile = Instantiate(playerProjectile,
                                    new Vector3(transform.position.x, transform.position.y - attackPoint, transform.position.z),
                                    Quaternion.identity);
            projectile.GetComponent<PlayerProjectileMovement>().SetVelocity(new Vector2(0, -projectSpeed));

        }
        else
        {
            GameObject projectile = Instantiate(playerProjectile,
                                    new Vector3(transform.position.x - attackPoint * (isFacingRight ? 1:-1), transform.position.y , transform.position.z),
                                    Quaternion.identity);
            projectile.GetComponent<PlayerProjectileMovement>().SetVelocity(new Vector2(projectSpeed * (isFacingRight ? 1 : -1), 0));

        }
        playerAnimator.SetTrigger("MeleeAttackTrigger");

        StartCoroutine(AttackCountdown());
    }


    private void Jump(InputAction.CallbackContext context)
    {
        jumpBufferCounter = jumpBufferTime;
    }
    private void PerformJump()
    {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            coyoteTimeCounter = 0;
            jumpBufferCounter = 0;
    }
    private void CancelJump(InputAction.CallbackContext context)
    {
        if (!isGrounded && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*0.6f);
    }

    private void Dash(InputAction.CallbackContext context)
    {
        if ( canDash && !isAttacking && canUseDash)
        {
            StartCoroutine(Dashing());
        }

    }
    private IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        playerAnimator.SetTrigger("DashTrigger");
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(dashingVelocity * (isFacingRight ? 1 : -1), 0f);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 6);

        yield return new WaitForSeconds(dashingTime);
        Physics2D.IgnoreLayerCollision(gameObject.layer, 6, false);

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


    public IEnumerator StopInputActions(float time)
    {
        DisableInput();
        isStopControl = true;
        yield return new WaitForSeconds(time);
        EnableInput();
        isStopControl = false;
    }

    public void SetVelocity(Vector2 velocity)
    {
        horizontal = velocity.x;
        rb.velocity = velocity;
    }

    public void UnlockAbility(int ability)
    {
        switch (ability)
        {
            case 1:
                canUseDash = true;
                break;
            case 2:
                extraJump = 1;
                break;
            default: break;
        }
    }
}
