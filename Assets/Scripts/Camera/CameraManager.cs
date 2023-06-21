using System.Collections;
using Cinemachine;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera[] allVirtualCamera;

    [SerializeField] private float fallPanAmout;
    [SerializeField] private float fallYPanTime;
    public float fallSpeedYDampingThreshold = -15.0f;

    public bool IsLerpingYDamping { get; private set; }
    public bool LerpedFromPlayerFalling { get;  set; }

    private Coroutine LerpYPanCoroutine;
    private Coroutine panCameraCoroutine;

    private CinemachineVirtualCamera currentCamera;
    private CinemachineFramingTransposer framingTransposer;

    private Vector2 startTrackedObjectOffset;

    private float normYPanAmount;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        for (int i = 0; i < allVirtualCamera.Length; i++)
        {
            if (allVirtualCamera[i].enabled)
            {
                currentCamera = allVirtualCamera[i];

                framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
            }
        }
        normYPanAmount = framingTransposer.m_YDamping;

        startTrackedObjectOffset = framingTransposer.m_TrackedObjectOffset;

    }

    
    #region Lerp Y Damping
    public void LerpYDamping(bool isPlayerFalling)
    {
        LerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }
    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampYAmount = framingTransposer.m_YDamping;
        float endDampAmount = 0f;

        // check if player is falling or not
        if(isPlayerFalling)
        {
            endDampAmount = fallPanAmout;
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = normYPanAmount;
        }

        //lerping
        float eslapsedTime = 0f;
        while (eslapsedTime <= fallYPanTime)
        {

            eslapsedTime += Time.deltaTime;

            float lerpPanAmount = math.lerp(startDampYAmount, endDampAmount, eslapsedTime/ fallYPanTime);
            
            framingTransposer.m_YDamping = lerpPanAmount;
            yield return null;
        }
        IsLerpingYDamping = false;
    }
    #endregion

    #region Pan Camera

    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 startPos = Vector2.zero;
        Vector2 endPos = Vector2.zero;

        if (!panToStartingPos)
        {
            switch (panDirection)
            {
                case PanDirection.Left:
                    endPos = Vector2.left;
                    break;
                case PanDirection.Right:
                    endPos = Vector2.right;
                    break;
                case PanDirection.Up:
                    endPos = Vector2.up;
                    break;
                case PanDirection.Down:
                    endPos = Vector2.down;
                    break;
                default:
                    break;

            }
            endPos *= panDistance;
        }

        else
        {
            startPos = framingTransposer.m_TrackedObjectOffset;
            endPos = startTrackedObjectOffset;
        }
        float eslapsedTime = 0f;
        while (eslapsedTime <= panTime)
        {
            eslapsedTime += Time.deltaTime;

            Vector3 panLerp = Vector3.Lerp(startPos, endPos, (eslapsedTime / panTime));
            framingTransposer.m_TrackedObjectOffset = panLerp;

            yield return null;
        }

    }

    #endregion

    #region Swap Camera
    public void SwapCamera(CinemachineVirtualCamera cameraFromLeft, CinemachineVirtualCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        if (currentCamera == cameraFromLeft && triggerExitDirection.x > 0)
        {
            //activate newCamera
            cameraFromRight.enabled = true;

            //Deactivate old camera
            cameraFromLeft.enabled = false;
            //set the new camera as the current camera
            currentCamera = cameraFromRight;
            //update composer variable

            framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        }
        else if(currentCamera == cameraFromRight && triggerExitDirection.x < 0)
        {
            //activate newCamera
            cameraFromLeft.enabled = true;

            //Deactivate old camera
            cameraFromRight.enabled = false;
            //set the new camera as the current camera
            currentCamera = cameraFromLeft;
            //update composer variable

            framingTransposer = currentCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
    
        }
    }
    #endregion
}
