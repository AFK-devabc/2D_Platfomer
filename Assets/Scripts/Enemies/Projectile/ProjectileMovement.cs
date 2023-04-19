using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class ProjectileMovement : MonoBehaviour
{

    Vector3 velocity = new Vector3(0,0,0);
    [SerializeField] private float gravity ;
    [SerializeField] private float startVelocity;
    Transform transform;
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        velocity.y += gravity*Time.deltaTime ;
        transform.position = transform.position + velocity * Time.deltaTime; 
    }

    public void SetTarget(Vector3 position)
    {
        Vector3 dis = position - transform.position ;

        velocity = new Vector2 (dis.x, dis.y);
        velocity = velocity.normalized * startVelocity;
    }
}
