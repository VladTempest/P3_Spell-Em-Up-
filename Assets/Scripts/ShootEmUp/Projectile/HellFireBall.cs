using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


namespace ShootEmUp.Projectile
{
    public class HellFireBall : BetterFireball
    {
        [SerializeField] private CircleCollider2D _collider;
        [SerializeField] private float _timeBeforeColliderOn=1f;
        [SerializeField] private float _minTimeBeforeDestroy=0.1f;
        [SerializeField] private float _maxTimeBeforeDestroy=1f;


        private void Awake()
        {
            StartCoroutine(TurnOnCollider());
            
        }

        IEnumerator TurnOnCollider()
        {
            yield return new WaitForSeconds(_timeBeforeColliderOn);
            _collider.isTrigger = false;
            StartCoroutine(DestroyHellFireBall());
        }

        IEnumerator DestroyHellFireBall()
        {
            yield return new WaitForSeconds(Random.Range(_minTimeBeforeDestroy, _maxTimeBeforeDestroy));
            CreateExplosionOnContact();
            DestroyThisProjectile();
        }
    }
}
