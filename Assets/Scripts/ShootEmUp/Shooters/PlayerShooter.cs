using System;
using ShootEmUp.GameConfigs;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ShootEmUp.Shooters
{
    public class PlayerShooter : Shooter,ISubscribers
    {
        [SerializeField]
        private protected double _timeBetweenShots = 0.4f;
        
        private float _fireTimer = 2f;
        
        private float _bulletDamagePlayer;
        private bool _isGameplayActive = true;

        private AnimationPlayer _playerAnimationPLayer = null;
        
        [SerializeField]
        private bool _isActivePCControls = false;
        [SerializeField]
        private Joystick _joystickAttack=null;
        [SerializeField]
        private float _deadZone = 0.3f;
        
        
        private void Awake()
        {
            _bulletDamagePlayer = GameManager.Instance.configsDictionary[ConfigsEnum.Configs.PlayerDamage];
            _playerAnimationPLayer = GetComponentInChildren<AnimationPlayer>();
        }


        private void FixedUpdate()
        {
            _fireTimer += Time.fixedDeltaTime;

            if (_isActivePCControls)
            {
                if (Input.GetButton("Fire1") && _isGameplayActive)
                {
                    if (EventSystem.current.IsPointerOverGameObject())
                        return;
                    if (_fireTimer >= _timeBetweenShots)
                    {
                        _fireTimer = 0;
                        _playerAnimationPLayer.StartAttackingAnimation();

                    }
                }
                if (Input.GetButtonUp("Fire1"))
                    _playerAnimationPLayer.StopAttackingAnimation();
            }
            else
            {
                if (!_isGameplayActive) return;
                if (CheckIfJoystickOffSetOverDeadzone() )
                {
                    
                    if (_fireTimer >= _timeBetweenShots)
                    {
                        _fireTimer = 0;
                        _playerAnimationPLayer.StartAttackingAnimation();

                    }
                }
                else
                    _playerAnimationPLayer.StopAttackingAnimation();
            }
            
        }

        public override GameObject ShootBullet(GameObject bulletPrefab, Transform firePoint)
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.FireBall_Burst,transformOfPlayPoint:transform);
            return base.ShootBullet(bulletPrefab, firePoint);
        }

        private bool CheckIfJoystickOffSetOverDeadzone()
        {
            var maxOffset = Math.Max(Math.Abs(_joystickAttack.Horizontal), Math.Abs(_joystickAttack.Vertical));
            return (maxOffset > _deadZone);
        }
     protected override float GetBulletDamage()
            {
                return _bulletDamagePlayer;
            }
     
        void CheckStateOfGame()
        {
            _isGameplayActive = GameManager.Instance.isGameActive;
        }
       
        public void Subscribe()
        {
            EventBroker.PlayerDied += CheckStateOfGame;
            EventBroker.PlayerWon += CheckStateOfGame;
        }
        public void Unsubscribe()
        {
            EventBroker.PlayerDied -= CheckStateOfGame;
            EventBroker.PlayerWon -= CheckStateOfGame;
        }
    }
}
