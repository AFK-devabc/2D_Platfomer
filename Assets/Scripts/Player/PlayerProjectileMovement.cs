using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerProjectileMovement : MonoBehaviour
{
    Vector3 velocity = new Vector3(0, 0, 0);
    private static float CollideDamage = 1.0f;
    private float lifeTime = 0.5f;
    Transform transform;
    [SerializeField] private GameObject projectile;
    [SerializeField] private LayerMask mask;
    [SerializeField] private float projectileRadius = 0.2f;
    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        lifeTime -= Time.fixedDeltaTime;
        if (lifeTime < 0) { Destroy(gameObject); }
    }
    private void Update()
    {
        RaycastHit2D info = Physics2D.CircleCast(transform.position ,projectileRadius,velocity.normalized,0f,mask);

        if (info.collider != null)
        {
            Debug.Log(info);

            if (info.collider.gameObject.tag == "Enemy")
            {
                info.collider.transform.GetComponent<Health>().TakeDamage(CollideDamage);
            }
            projectile.transform.SetParent(null);
            Destroy(projectile, 1.0f);
            Destroy(gameObject);
        }

    }
    public void SetVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
    }
}
