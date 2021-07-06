using System;
using ShootEmUp.Characteristics;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Projectile;
using UnityEngine;
namespace ShootEmUp.Shooters
{
    public abstract class Shooter : MonoBehaviour
    {
        [SerializeField]
        private Transform _firePoint;
        public GameObject _bullet;

        [SerializeField]
        public float _bulletForce=20f;

        
        



        protected abstract float GetBulletDamage();
        
        public void ShootBulletFromFirePoint()
        {
            GameObject bullet = ShootBullet(_bullet, _firePoint);
            if (bullet.GetComponent<Rigidbody2D>() == null) return; 
                Rigidbody2D bulletRigidBody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidBody.AddForce(_firePoint.up * _bulletForce, ForceMode2D.Impulse);
            
        }

        public virtual GameObject ShootBullet(GameObject bulletPrefab, Transform firePoint)
        {
            GameObject bullet =Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            ProjectileClass projectileClassComponent = bullet.GetComponent<ProjectileClass>();
            projectileClassComponent.bulletDamage = GetBulletDamage();
            return bullet;
        }


        
    }
}
