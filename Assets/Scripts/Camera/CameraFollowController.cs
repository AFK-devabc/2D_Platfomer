using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{

    [SerializeField] private Transform followObject;
    [SerializeField] private float flipRotationTime = 0.5f;

    private Coroutine turnCoroutine;
    
    void Update()
    {
        this.transform.position = followObject.position;
    }

    public void CallTurn()
    {
        turnCoroutine = StartCoroutine(FlipYLerp());
    }

    private IEnumerator FlipYLerp()
    {
        float startRotation = transform.localEulerAngles.y;
        float endRotation = followObject.localEulerAngles.y;

        float yRotation = 0f;
        float elapstedTime = 0f;
        while (elapstedTime < flipRotationTime)
        {
            elapstedTime += Time.deltaTime;

            yRotation = Mathf.Lerp(startRotation, endRotation, elapstedTime / flipRotationTime);
            transform.rotation = Quaternion.Euler(transform.rotation.x, yRotation, transform.rotation.y);
            yield return null;
        }
    }
}
