using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float movementSpeed;
    protected Transform playerInRange;

    protected Rigidbody2D rb;

    [SerializeField] private bool canMove;

    [SerializeField] private bool stopped;
    [SerializeField] private bool noFlipping;


    private void FixedUpdate()
    {
        if(stopped) 
            return;
        if(canMove)
        {
            if(playerInRange)
            {
                
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
        if(playerInRange)
        {
            float dir = playerInRange.position.x- transform.position.x;


        }
    }

}
