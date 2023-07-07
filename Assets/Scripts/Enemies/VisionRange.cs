using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionRange : MonoBehaviour
{
    [SerializeField] private float visionRange;

    private bool inRange = false;

    private void Update()
    {

        if (inRange)
            return;

        Collider2D coll = Physics2D.OverlapCircle(transform.position, visionRange, LayerMask.GetMask("Player"));
        if (!coll)
            return;
        Vector3 endPos = coll.transform.position;
        Vector3 dir = endPos - transform.position;
        float dist = Vector2.Distance(transform.position, endPos);
        // check if player behind a walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, dist, LayerMask.GetMask("Platform"));
        if (hit)
            return;
        
        transform.BroadcastMessage("SetTarget", coll.transform, SendMessageOptions.DontRequireReceiver);
        inRange  = true;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.position, visionRange);
    //}
}
