using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DampedMove : MonoBehaviour
{
    [SerializeField] private Transform toTrack;
    private float dist = 2;
    public float speed = 20;

    Vector3 desiredPos;
    public bool unParent;
        
        void Start()
    {
        if (unParent)
            transform.SetParent(null);
        if (!toTrack)
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position =  Vector3.Lerp(transform.position, toTrack.position, Time.deltaTime * speed);

        transform.rotation = Quaternion.Euler(Vector3.Lerp( transform.eulerAngles, toTrack.eulerAngles ,Time.deltaTime * speed));

    }
}
    