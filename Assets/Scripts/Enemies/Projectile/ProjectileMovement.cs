using UnityEngine;
using UnityEngine.UI;

public class ProjectileMovement : MonoBehaviour
{

    Vector3 velocity = new Vector3(0,0,0);
    [SerializeField] private float startVelocity;
    private static float CollideDamage = 1.0f;
    private float lifeTime = 2.0f;
    Transform transform;
    [SerializeField] private LayerMask mask;

    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileRadius = 0.2f;

    private void Awake()
    {
        transform = GetComponent<Transform>();
    }
    // Update is called once per frame

    private void Update()
    {
        RaycastHit2D info = Physics2D.CircleCast(transform.position, projectileRadius, velocity.normalized, 0f, mask);
        if (info.collider != null)
        {
            if (info.collider.gameObject.tag == "Player")
            {
                info.collider.transform.GetComponent<Health>().TakeDamage(CollideDamage, this.transform);
            }
            projectile.transform.SetParent(null);
            Destroy(projectile, 1.0f);
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        transform.position = transform.position + velocity * Time.deltaTime;
        lifeTime -= Time.fixedDeltaTime;
        if(lifeTime < 0 ) { Destroy( gameObject ); }
    }

    public void SetTarget(Vector3 position)
    {
        Vector3 dis = position - transform.position ;

        velocity = new Vector2 (dis.x, dis.y);
        velocity = velocity.normalized * startVelocity;
    }
}
