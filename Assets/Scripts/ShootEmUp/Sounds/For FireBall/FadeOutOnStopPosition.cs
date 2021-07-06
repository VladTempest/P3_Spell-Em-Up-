using System;
using System.Collections;
using UnityEngine;
namespace ShootEmUp.Sounds.For_FireBall
{
    public class FadeOutOnStopPosition : MonoBehaviour
    {
        private Coroutine _volumeFadeOut;
        private bool _isCoroutineActive = false;

        private float _volume;
        
        [SerializeField]
        private int _stepsOfFading;
        [SerializeField]
        private float _fadingDuration;
        
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private ParticleSystem _particleSystem;


        private void Awake()
        {
            _volume = _audioSource.volume;
        }
        private void LateUpdate()
        {
            if (!_isCoroutineActive && _particleSystem.emission.enabled==false)
            {
                StartCoroutine(VolumeFadeOut());
            }
        }

        IEnumerator VolumeFadeOut()
        {
            _isCoroutineActive = true;
            
            var stepOfVolumeFading = _volume / _stepsOfFading;
            var timeStep = _fadingDuration / _stepsOfFading;

            for (int i = 0; i <= _stepsOfFading; i++)
            {
                _audioSource.volume -= stepOfVolumeFading;
                yield return new WaitForSeconds(timeStep);
            }
            

        }

        private void OnDestroy()
        {
            if (_volumeFadeOut != null)
            {
                StopCoroutine(_volumeFadeOut);
            }
        }



    }
}
