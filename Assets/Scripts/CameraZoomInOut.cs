using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraZoomInOut : MonoBehaviour
{
    public CinemachineVirtualCamera cinemachineCamera;
    public float zoomSpeed = 10f;
    public float minOrthoSize = 5f;
    public float maxOrthoSize = 20f;

    void Start()
    {
        cinemachineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cinemachineCamera.m_Lens.OrthographicSize -= scroll * zoomSpeed;
        cinemachineCamera.m_Lens.OrthographicSize = Mathf.Clamp(cinemachineCamera.m_Lens.OrthographicSize, minOrthoSize, maxOrthoSize);
    }
}
