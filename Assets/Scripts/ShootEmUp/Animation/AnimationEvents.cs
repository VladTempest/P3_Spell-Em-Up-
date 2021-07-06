using ShootEmUp.EnemyAttacker;
using ShootEmUp.Shooters;
using ShootEmUp.Sounds;
using UnityEngine;
namespace ShootEmUp.Animation
{
    public class AnimationEvents : MonoBehaviour
    {
        [SerializeField]
        private Shooter _shooter = null;
        [SerializeField]
        private MeleeEnemyAttacker _meleeEnemyAttacker=null;

       
        public void ShootProjectile()
        {
            if (_shooter == null) return;
            _shooter.ShootBulletFromFirePoint();
        }
        
        public void CutWithMeleeWeapon()
        {
            if (_meleeEnemyAttacker == null) return;
            _meleeEnemyAttacker.CutWithMeleeWeapon();
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Mellee_Atack,transformOfPlayPoint:transform);
        } 

        public void PlaySound(TypeOfSFXByItsNature typeOfSfxByItsNature)
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:typeOfSfxByItsNature,transformOfPlayPoint:transform);
        }
        
        
    }
}
