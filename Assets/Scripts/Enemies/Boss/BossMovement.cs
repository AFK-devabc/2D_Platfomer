using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : EnemyMovement
{
    [SerializeField] protected float movementSpeed;
    
    float timeAppear = 0;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Sleep");
    }

    public override void NormalMovement()
    {
        Debug.Log("normal movement");
    }

    public override void CombatMovement()
    {
        if (timeAppear > 2.0f)
        {
            if (movementSpeed * (rb.transform.position.x - attackTarget.position.x) > 0)
            {
                Flipping();
                movementSpeed *= -1;
            }
            rb.velocity = new Vector2(movementSpeed, rb.velocity.y);
        }
        else timeAppear += Time.deltaTime;
    }

    protected  void Flipping()
    {
        if (movementSpeed > 0)
            rb.transform.rotation = Quaternion.Euler(0, -10, 0);
        else
            rb.transform.rotation = Quaternion.Euler(0, -140, 0);
    }

    //public override void SetTarget(Transform target)
    //{
    //    attackTarget = target;
    //    transform.GetChild(0).GetComponent<Animator>().SetTrigger("Appear");
    //    stopped = false;
    //    Debug.Log("appear");
    //}
}
