using System;
using System.Collections;
using ShootEmUp.FX;
using ShootEmUp.Player;
using UnityEngine;
using Light2D = UnityEngine.Experimental.Rendering.Universal.Light2D;

namespace ShootEmUp.FogOfWarSystem
{
    public class LightAmbientTurning : MonoBehaviour
    {
        private bool _isRoomLit = false;
        
        [SerializeField]
        private BoxCollider2D _triggerCollider;
        [SerializeField]
        private bool _isTurnOnByTrigger = false;
        [SerializeField]
        private float _timeToFullyLight;
        [SerializeField]
        private int _stepsOfSettingIntensity;
        [SerializeField]
        private LightAmbientTurning[] _allTheLightsInRoom;

        public Light2D lightOfRoom;
        public float finalLightIntensity;
        public  FXContrlEvent fxContrl;
        
        

        private void Awake()
        {
            lightOfRoom = GetComponent<Light2D>();
            if (_isTurnOnByTrigger)
            {
                _triggerCollider = GetComponent<BoxCollider2D>();
                
            }
            
            
        }
        private void Start()
        {
            if (_isTurnOnByTrigger)
            {
                GetAllTheLightsInRoom();
                TurnOffEveryLightInRoom();
            }
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if (!_isRoomLit&&(other.gameObject.GetComponent<PlayerController>()!=null))
            {
                TurnOnEveryLightInRoom();
                
            }
            
            
        }

        public IEnumerator SetIntensityToFinalValue(float timeToFullyLight)
        {
            _isRoomLit = true;
            var intensityStepIncrease = finalLightIntensity / _stepsOfSettingIntensity;
            var timeSteps = timeToFullyLight / _stepsOfSettingIntensity;
            var currentIntensity = lightOfRoom.intensity;
            while (currentIntensity < finalLightIntensity)
            {
                currentIntensity += intensityStepIncrease;
                lightOfRoom.intensity = currentIntensity;
                yield return new WaitForSeconds(timeSteps);
            }
        }

        private void GetAllTheLightsInRoom()
        {
            _allTheLightsInRoom = GetComponentsInChildren<LightAmbientTurning>();
        }

        private void TurnOnEveryLightInRoom()
        {
            
            foreach (LightAmbientTurning lightInRoom in _allTheLightsInRoom)
            {
                if (lightInRoom.fxContrl!=null) lightInRoom.fxContrl.PlayCertainFX();
                StartCoroutine(lightInRoom.SetIntensityToFinalValue(_timeToFullyLight));
            }
        }
        
        private void TurnOffEveryLightInRoom()
        {
           
            foreach (LightAmbientTurning lightInRoom in _allTheLightsInRoom)
            { if (lightInRoom.fxContrl!=null) lightInRoom.fxContrl.PauseCertainFX();
                lightInRoom.finalLightIntensity = lightInRoom.lightOfRoom.intensity;
                lightInRoom.lightOfRoom.intensity = 0;
            }
        }
        
        
    }
}
