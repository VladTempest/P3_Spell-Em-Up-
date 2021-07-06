using ShootEmUp.Shooters;
using UnityEngine;
namespace ShootEmUp.EnemyAttacker
{
    [RequireComponent(typeof(EnemyShooter))]
    public class RangedEnemyAttacker : EnemyAttacker
    {
        private EnemyShooter _shooter;
        public void Awake()
        {
            base.Awake();
            _shooter = GetComponent<EnemyShooter>();
        }

        protected internal override void Attack()
        {
            base.Attack();
            _shooter.isInFireState = true;
        }
        
        protected internal override void StopAttack()
        {
            base.StopAttack();
            _shooter.isInFireState = false;
        }

    }
}
