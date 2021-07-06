using ShootEmUp.Animation;
using ShootEmUp.EnemyAttacker;
using ShootEmUp.FX;
using ShootEmUp.GameConfigs;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Projectile;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Characteristics
{
    public class EnemyCharacteristics : CharacterCharacteristics, ISubscribers
    {
        public override void Awake()
        {
            base.Awake();
            _maxHealth = GameManager.Instance.configsDictionary[ConfigsEnum.Configs.EnemiesHealth] * _maxHealthPointsCoefficient;
        }
        void OnEnable()
        {
            Subscribe();
        }
        private void OnDestroy()
        {
            Unsubscribe();
        }
        protected override void DestroyCharacter()
        {
            
            InWorldFXPlayer.Instance.InstantiateFX(InWorldFXs.BodyExplosion,transform.position);
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Enemy_MeatSplash,transformOfPlayPoint:transform);
            EventBroker.CallEnemyDied();
            Destroy(gameObject);
        }
        public override void OnCollisionEnter2D(Collision2D other)
        {
            ProjectileClass projectileClass = other.gameObject.GetComponent<ProjectileClass>();
            if (projectileClass == null) return;
            
                ReduceHealth(projectileClass.bulletDamage);
        }
        
        void HideAfterEndGame()
        {
            gameObject.SetActive(false);
        }
        public void Subscribe()
        {
            EventBroker.PlayerWon += HideAfterEndGame;
            EventBroker.PlayerDied += HideAfterEndGame;
        }
        public void Unsubscribe()
        {
            EventBroker.PlayerWon -= HideAfterEndGame;
            EventBroker.PlayerDied -= HideAfterEndGame;
        }
    }
}
