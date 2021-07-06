using System.Collections;
using UnityEngine;

namespace ShootEmUp.EnemyAttacker
{
    public class MeleeEnemyAttacker : EnemyAttacker
    {
        
        public void CutWithMeleeWeapon()
        {
            _playerCharacteristics.ReduceHealth(attackDamage);
            
        }
        
        
    }
}
