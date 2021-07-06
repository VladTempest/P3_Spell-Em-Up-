using System;
using System.Collections;
using ShootEmUp.FX;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Projectile
{
    public class FireSplash : ProjectileClass
    {
        [SerializeField]
        private CircleCollider2D _projectileCollider;
        [SerializeField]
        private float _finalColliderRadius=2f;
        [SerializeField]
        private int _numberOfExpansionSteps=10;

        private Coroutine _colliderExpansionCoroutine;
        private float _startColliderRadius;
        private void OnCollisionEnter2D(Collision2D other)
        {
            DestroyThisProjectile();
        }
        
        private void Awake()
        {
            DestroyThisProjectile();
            _startColliderRadius = _projectileCollider.radius;
            _colliderExpansionCoroutine=StartCoroutine(ExpandCollider());
           
        }


        public override void DestroyThisProjectile()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.FireBall_Explosion,transformOfPlayPoint:transform);
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
