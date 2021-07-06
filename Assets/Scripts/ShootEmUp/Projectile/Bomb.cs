using System;
using System.Collections;
using ShootEmUp.FX;
using ShootEmUp.Sounds;
using UnityEngine;
namespace ShootEmUp.Projectile
{
    public class Bomb : ProjectileClass
    {
        [SerializeField]
        private CircleCollider2D _projectileCollider;
        [SerializeField]
        private float _finalColliderRadius=2f;
        [SerializeField]
        private int _numberOfExpansionSteps=10;

        private Coroutine _colliderExpansionCoroutine;
        private float _startColliderRadius;


        private void Awake()
        {
            _startColliderRadius = _projectileCollider.radius;
            _colliderExpansionCoroutine=StartCoroutine(ExpandCollider());
        }
        private void Start()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Bomber_Explosion,transformOfPlayPoint:transform);
            DestroyThisProjectile();
        }

        public override void DestroyThisProjectile()
        {
            InWorldFXPlayer.Instance.PlayFXCameraShake();
            base.DestroyThisProjectile();
        }
        IEnumerator ExpandCollider()
        {
            var pauseBetweenExpansion = _destroyTimer*0.8f / _numberOfExpansionSteps;
            var stepOfExpansion = (_finalColliderRadius - _startColliderRadius) / _numberOfExpansionSteps;
            
            while (_startColliderRadius<_finalColliderRadius)
            {
                _startColliderRadius += stepOfExpansion;
                _projectileCollider.radius = _startColliderRadius;
                yield return new WaitForSeconds(pauseBetweenExpansion);
            }
        }
        

        private void OnDestroy()
        {
            if (_colliderExpansionCoroutine != null)
            {
                StopCoroutine(_colliderExpansionCoroutine);
            }
        }
    }
}
