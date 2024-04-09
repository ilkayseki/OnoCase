using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCameraBase camera1;
    public CinemachineVirtualCameraBase camera2;

    private void OnEnable()
    {
        EventController.MachineChoseAction += SwitchMachineCamera;
        EventController.BackChoseAction += SwitchUICamera;
    }

    private void OnDisable()
    {
        EventController.MachineChoseAction -= SwitchMachineCamera;
        EventController.BackChoseAction -= SwitchUICamera;
    }

    void Start()
    {
        // İlk başta kamera 1'i etkinleştirin
        camera1.Priority = 10;
        camera2.Priority = 0;
    }

    private void SwitchMachineCamera()
    {
        // Kamera 1'i devre dışı bırakın, kamera 2'yi etkinleştirin
        camera1.Priority = 0;
        camera2.Priority = 10;
    }
    
    private void SwitchUICamera()
    {
        // Kamera 1'i devre dışı bırakın, kamera 2'yi etkinleştirin
        camera1.Priority = 10;
        camera2.Priority = 0;
    }
    
}
