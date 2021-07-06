using ShootEmUp.FX;
using ShootEmUp.GameConfigs;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Projectile;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Characteristics
{
    public class PlayerCharacterstics : CharacterCharacteristics
    {
        private TintEffectController _tintEffectController;
        [SerializeField]
        private GameObject _playerVisuals=null;
        
        
        public override void Awake()
        {
            base.Awake();
            _maxHealth = GameManager.Instance.configsDictionary[ConfigsEnum.Configs.PlayerHealth];
            EventBroker.CallPlayerSpawned();
            _tintEffectController = GetComponentInChildren<TintEffectController>();

        }
        protected override void DestroyCharacter()
        {
            if (_playerVisuals.activeSelf)
            {
                InWorldFXPlayer.Instance.InstantiateFX(InWorldFXs.BodyExplosion,transform.position);
                _playerVisuals.SetActive(false);
                EventBroker.CallPlayerDied();
            }
        }
        
        public override void OnCollisionEnter2D(Collision2D other)
        {
            
            ProjectileClass projectileClass = other.gameObject.GetComponent<ProjectileClass>();
            if (projectileClass == null) return;
            if (!ChekIfBulletShotByPlayer(projectileClass))
            {
                ReduceHealth(projectileClass.bulletDamage);
            }
        }
        
        private bool ChekIfBulletShotByPlayer(ProjectileClass projectileClass)
        {
            return projectileClass != null && (projectileClass._projectileType == Projectiles.FireBall|| projectileClass._projectileType == Projectiles.FireSplash||projectileClass._projectileType == Projectiles.BetterFireBall||projectileClass._projectileType == Projectiles.HellFireBall);
        }
        
        public override void ReduceHealth(float damageValue)
        {
            base.ReduceHealth(damageValue);
            if (GameManager.Instance.isGameActive)
            {
                SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Player_MeatSplash,transformOfPlayPoint:transform);
                InWorldFXPlayer.Instance.InstantiateFX(InWorldFXs.BloodBurst, transform.position, transform);
                _tintEffectController.StartTintCoroutine();
            }
        }

        
    }
}
