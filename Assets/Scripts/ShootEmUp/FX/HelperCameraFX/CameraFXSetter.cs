using System;
using Cinemachine;
using UnityEngine;
namespace ShootEmUp.FX.HelperCameraFX
{
    public class CameraFXSetter : MonoBehaviour
    {
        private void Awake()
        {
            var virtualCamera = GetComponent<CinemachineVirtualCamera>();
            InWorldFXPlayer.Instance._virtualCamera = virtualCamera;
            InWorldFXPlayer.Instance. cinemachineBasicMultiChannelPerlin= virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
}
