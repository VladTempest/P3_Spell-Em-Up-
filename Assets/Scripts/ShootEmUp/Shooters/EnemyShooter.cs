using ShootEmUp.EnemyAttacker;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShootEmUp.Shooters
{
    [RequireComponent(typeof(EnemyAttacker.EnemyAttacker))]
    public class EnemyShooter : Shooter
    {
        private float _bulletDamageEnemy=1f;
        public bool isInFireState = false;

        void Start()
        {
            _bulletDamageEnemy = GetComponent<EnemyAttacker.EnemyAttacker>().attackDamage;
        }
     
        protected override float GetBulletDamage()
        {
            return _bulletDamageEnemy;
        }
        
    }
}
