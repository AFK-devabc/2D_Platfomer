using Cinemachine;
using UnityEngine;
using UnityEditor;
public class CameraControllerTrigger : MonoBehaviour
{
    public CustomCameraInspertorObjects customCameraInspertorObjects;

    private Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {

            if (customCameraInspertorObjects.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(customCameraInspertorObjects.panDistance,
                    customCameraInspertorObjects.panTime,
                    customCameraInspertorObjects.panDirection, false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // swap the camera
            if (customCameraInspertorObjects.swapCameras &&
                customCameraInspertorObjects.camOnLeft != null &&
                customCameraInspertorObjects.camOnRight != null)
            {
                Vector2 exitDirection = (collision.transform.position - coll.bounds.center).normalized;

                CameraManager.instance.SwapCamera(customCameraInspertorObjects.camOnLeft, customCameraInspertorObjects.camOnRight, exitDirection);
            }

            //Pan camera in direction
            if (customCameraInspertorObjects.panCameraOnContact)
            {
                    CameraManager.instance.PanCameraOnContact(customCameraInspertorObjects.panDistance,
                    customCameraInspertorObjects.panTime,
                    customCameraInspertorObjects.panDirection, true);

            }
        }
    }
}


[System.Serializable]

public class CustomCameraInspertorObjects
{
    public bool swapCameras = false;
    public bool panCameraOnContact = false;

    [HideInInspector] public CinemachineVirtualCamera camOnLeft;
    [HideInInspector] public CinemachineVirtualCamera camOnRight;

    [HideInInspector] public PanDirection panDirection;
    [HideInInspector] public float panDistance = 3f;
    [HideInInspector] public float panTime = 0.35f;

}

public enum PanDirection
{
    Up, 
    Down,
    Left, 
    Right
}

#if UNITY_EDITOR

[CustomEditor(typeof(CameraControllerTrigger))]
public class CameraEditor : Editor
{
    CameraControllerTrigger cameraControllerTrigger;
    private void OnEnable()
    {
        cameraControllerTrigger = (CameraControllerTrigger)target;
    }

    public override void OnInspectorGUI()
    {
       DrawDefaultInspector();
        if(cameraControllerTrigger.customCameraInspertorObjects.swapCameras)
        {
            cameraControllerTrigger.customCameraInspertorObjects.camOnLeft 
                    = EditorGUILayout.ObjectField("Camera on Left", 
                      cameraControllerTrigger.customCameraInspertorObjects.camOnLeft,
                      typeof(CinemachineVirtualCamera),
                      true) as CinemachineVirtualCamera;

            cameraControllerTrigger.customCameraInspertorObjects.camOnRight
                    = EditorGUILayout.ObjectField("Camera on Right",
                      cameraControllerTrigger.customCameraInspertorObjects.camOnRight,
                      typeof(CinemachineVirtualCamera),
                      true) as CinemachineVirtualCamera;
        }

        if (cameraControllerTrigger.customCameraInspertorObjects.panCameraOnContact)
        {
            cameraControllerTrigger.customCameraInspertorObjects.panDirection
                      = (PanDirection)EditorGUILayout.EnumPopup("Direction",
                        cameraControllerTrigger.customCameraInspertorObjects.panDirection );

            cameraControllerTrigger.customCameraInspertorObjects.panDistance
                    = EditorGUILayout.FloatField("Pan Distance",
                    cameraControllerTrigger.customCameraInspertorObjects.panDistance);
            cameraControllerTrigger.customCameraInspertorObjects.panTime
                    = EditorGUILayout.FloatField("Pan Time",
                    cameraControllerTrigger.customCameraInspertorObjects.panTime);
        }

        if(GUI.changed)
        {
            EditorUtility.SetDirty(cameraControllerTrigger );
        }
    }
}
#endif