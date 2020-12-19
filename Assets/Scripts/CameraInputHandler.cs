using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputHandler : MonoBehaviour
{
    private CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        var pointerDelta = context.ReadValue<Vector2>();
        freeLookCamera.m_XAxis.Value += pointerDelta.x;
        freeLookCamera.m_YAxis.Value += pointerDelta.y;
    }
}
