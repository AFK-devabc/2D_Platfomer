using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyEnemyMovement : EnemyMovement
{

    [SerializeField] private Transform realMesh;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float minDistX;
    [SerializeField] private float MaxDistX;
    [SerializeField] private float minDistY;
    [SerializeField] private float MaxDistY;
    public override  void NormalMovement()
    {
    }

    public override void CombatMovement()
    {

        float distX = attackTarget.position.x - transform.position.x;
        float distY = attackTarget.position.y - transform.position.y;
            
        //Rotate gameobject to face player
        Vector3 euler = realMesh.eulerAngles;
        euler.y = distX > 0 ? 120 : 240;
        realMesh.eulerAngles =Vector3.Lerp(realMesh.eulerAngles, euler, Time.deltaTime * rotationSpeed);

        //Make enemy keep distance with player
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * movementSpeed;
        if (Mathf.Abs(distX) <= minDistX)
        {
            velocity.x = 2 * movementSpeed * (distX > 0 ? 1 : -1);
        }
        else if (Mathf.Abs(distX) >= MaxDistX)
        {
            velocity.x = movementSpeed * (distX > 0 ? 1 : -1);
        }

        if (Mathf.Abs(distY) < minDistY)
        {
            velocity.y = 2 * movementSpeed;
        }
        else if (Mathf.Abs(distY) >= MaxDistY)
        {
            velocity.y = - movementSpeed ;
        }
    }
}
