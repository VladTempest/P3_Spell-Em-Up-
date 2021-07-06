using System;
using ShootEmUp.FX;
using ShootEmUp.Managers;
using UnityEngine;

namespace ShootEmUp.Projectile
{
    public class BetterFireball : ProjectileClass
    {
        [SerializeField]
        private protected Rigidbody2D _rigidBody;

        [SerializeField]
        private GameObject _finalFireSplash = null;

        private void OnCollisionEnter2D(Collision2D other)
        {
            DestroyThisProjectile();
            CreateExplosionOnContact();
        }



        protected void CreateExplosionOnContact()
        {
            Instantiate(_finalFireSplash, transform.position, transform.rotation);
            try
            {
                if (!GameManager.Instance.isGameActive) return;
            }
            catch (Exception e)
            {
                Debug.Log("GameManager while destroyng " + gameObject.name);
            }
           
        }
    
    }
}
