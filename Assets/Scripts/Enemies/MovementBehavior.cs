using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MovementBehavior
{
    public void Move(Rigidbody2D rb, Animator ani);
}
