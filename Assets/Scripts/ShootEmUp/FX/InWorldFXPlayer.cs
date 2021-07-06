using System;
using System.Collections.Generic;
using Cinemachine;
using ShootEmUp.Settings;
using UnityEngine;

namespace ShootEmUp.FX
{
    public class InWorldFXPlayer : SingletonBase<InWorldFXPlayer>
    { 
        public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
        public CinemachineVirtualCamera _virtualCamera;
        [SerializeField]
        private float _shakeIntencity = 1f;
        [SerializeField]
        private float _shakeDuration = 1f;
        private float shakeTimer;
        public InWorldFXListSettings _inWorldFXList = null;
        

       

        private void Awake()
        {
            var inWorldFXPlayer = InWorldFXPlayer.Instance;
            cinemachineBasicMultiChannelPerlin = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        public void InstantiateFX(InWorldFXs typeOfFX, Vector3 position)
        {
            Instantiate(_inWorldFXList.list[(int)typeOfFX], position, transform.rotation);
        }
        
        public void InstantiateFX(InWorldFXs typeOfFX, Vector3 position, Transform transform)
        {
            var parent = transform;
            Instantiate(_inWorldFXList.list[(int)typeOfFX], position, parent.rotation,parent);
        }

        public void PlayFXCameraShake()
        {
            ShakeCamera(_shakeIntencity,_shakeDuration);
        }

        
        private void ShakeCamera(float intensity, float time)
        {
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
            shakeTimer = time;
        }

        private void Update()
        {
            if (shakeTimer > 0)
            {
                shakeTimer -= Time.deltaTime;
                if (shakeTimer <= 0f)
                {
                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
                }
            }   
        }
    }
}
