using System;
using System.Collections;
using ShootEmUp.Projectile;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Animation.InWorldAnimationTriggers
{
    public class DoorOpener : MonoBehaviour
    {
        private Coroutine _doorOpening;
        private Quaternion _doorRotation;

        [SerializeField]
        private bool _isOpened = false;
        [SerializeField]
        private Transform _doorTransform;
        [SerializeField]
        private float _finalRotation;
        [SerializeField]
        private float _timeForOpening = 0.2f;
        [SerializeField]
        private int _stepsOfOpening = 50;
        [SerializeField]
        private BoxCollider2D _doorCollider;
        [SerializeField]
        private float _finalYSize = 0.68f;
        [SerializeField]
        private GameObject _lightUnderDoor = null;

        private void Awake()
        {
            _doorRotation = _doorTransform.rotation;
            _finalRotation = _doorRotation.eulerAngles.z;

            CloseDoor();
        }


        private void CloseDoor()
        {
            _isOpened = false;
            _doorTransform.Rotate(new Vector3(0, 0, -_finalRotation));
        }

        private void OpenDoor()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Door_Open,transformOfPlayPoint:transform);
            if (_doorOpening != null)
            {
                StopCoroutine(_doorOpening);
            }
            _doorOpening = StartCoroutine(OpenDoorCoroutine());
            SetFinalColliderSizeAndOffset();
            _lightUnderDoor.SetActive(false);
        }

        IEnumerator OpenDoorCoroutine()
        {
            _isOpened = true;
            var timeStep = _timeForOpening / _stepsOfOpening;
            var angleStep = (360f - _finalRotation) / _stepsOfOpening;
            var currentStep = 0;
            while (currentStep < _stepsOfOpening)
            {
                currentStep++;
                yield return new WaitForSeconds(0);
                _doorTransform.Rotate(new Vector3(0, 0, -angleStep));

            }
        }

        private void SetFinalColliderSizeAndOffset()
        {
            var finalColliderSize = new Vector2(_doorCollider.size.x, _finalYSize);
            var finalColliderOffset = new Vector2(_doorCollider.offset.x, 0);
            _doorCollider.size = finalColliderSize;
            _doorCollider.offset = finalColliderOffset;

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isOpened)
            {
                var projectile = other.gameObject.GetComponent<ProjectileClass>();
                if (projectile != null)
                {
                    OpenDoor();
                }
            }

        }


    }
}
