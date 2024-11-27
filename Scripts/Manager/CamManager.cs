using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Cams 
{
    Title, PlayerFollow, Front, Back, Piano, max
}

public class CamManager : MonoBehaviour
{
    public Cams camState;
    public List<CinemachineVirtualCamera> virtualCameras;
    private CinemachineVirtualCamera currentCamera;


    float zoomSpeed = 150f;
    float minFOV = 15f;
    float maxFOV = 80f;

    private void Start()
    {
        if (virtualCameras.Count > 0)
        {
            SetCamera(Cams.Title);
        }
    }

    public void SetCamera(Cams camIndex)
    {
        if ((int)camIndex >= (int)Cams.max)
        {
            Debug.LogWarning("잘못된 카메라 인덱스입니다.");
            return;
        }

        if (currentCamera != null)
        {
            currentCamera.Priority = 0;
        }

        currentCamera = virtualCameras[(int)camIndex];
        currentCamera.Priority = 10;
        camState = camIndex;
    }

    public void ZoomIn()
    {
        if (currentCamera != null)
        {
            float newFOV = Mathf.Max(minFOV, currentCamera.m_Lens.FieldOfView -  Time.deltaTime * zoomSpeed);
            currentCamera.m_Lens.FieldOfView = newFOV;
        }
    }

    public void ZoomOut()
    {
        if (currentCamera != null)
        {
            float newFOV = Mathf.Min(maxFOV, currentCamera.m_Lens.FieldOfView + Time.deltaTime * zoomSpeed);
            currentCamera.m_Lens.FieldOfView = newFOV;
        }
    }

    public float GetZoomValue() 
    {
        return currentCamera.m_Lens.FieldOfView;
    }
}
