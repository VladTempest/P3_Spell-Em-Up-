using System;
using System.Collections.Generic;
using ShootEmUp.Characteristics;
using ShootEmUp.EnemyAttacker;
using ShootEmUp.FX;
using ShootEmUp.Managers;
using UnityEngine;

namespace ShootEmUp.Projectile
{
    [RequireComponent(typeof(FXContrlEvent))]
    public class ProjectileClass : MonoBehaviour
    {
        [SerializeField]
        protected internal Projectiles _projectileType=Projectiles.FireBall;
        [SerializeField]
        private protected FXContrlEvent _fxControl = null;
        
        [SerializeField]
        private protected float _destroyTimer = 0f;
        [SerializeField] private bool _isPauseOfFXBeforeDestroyNeeded = true;
        
        public float bulletDamage;

        
        public virtual void DestroyThisProjectile()
        {

            for (int i = 0; i < _fxControl._fxObject.Count; i++)
            {
               _fxControl.SaveParticlesWithDestroyingAfterDelay(i,_isPauseOfFXBeforeDestroyNeeded);
            }

            Destroy(gameObject, _destroyTimer);
        }


    }
}
