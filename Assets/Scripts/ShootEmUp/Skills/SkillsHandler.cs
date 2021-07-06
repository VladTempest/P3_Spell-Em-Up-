using System;
using System.Collections;
using ShootEmUp.FX;
using ShootEmUp.Shooters;
using ShootEmUp.Spawners;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp.Skills
{
    [RequireComponent(typeof(Shooter))]
    public class SkillsHandler : MonoBehaviour
    {
        private Coroutine _changeProjectileCoroutine;
        private Coroutine _hellFireCoroutine;

        public float[] _skillsManaCost;
        [SerializeField]
        public float[] _skillsDuration;
        [SerializeField]
        public float[] _skillsCooldown;
        

        [SerializeField]
        private GameObject _fireSplashProjectile=null;

        private AnimationPlayer _animationPlayer = null;
        private FXContrlEvent _fxContrlEvent = null;
        [SerializeField]
        private GameObject _betterFireballProjectile=null;
        [SerializeField]
        private bool _isBetterFireBallInProgress=false;
        [SerializeField]
        private HellFireBallSpawner _hellFireBallSpawner;

        [SerializeField]
        private bool _isHellFireInProgress = false;

        [SerializeField]
        private Shooter _shooter=null;

        private void Awake()
        {
            _shooter = GetComponentInChildren<Shooter>();
            _fxContrlEvent = GetComponentInChildren<FXContrlEvent>();
            _animationPlayer = GetComponentInChildren<AnimationPlayer>();
            
            _fxContrlEvent.PauseCertainFX(2);


        }

        public void ActivateSkill(SkillsEnum skill)
        {
            switch (skill)
            {
                case (SkillsEnum.FireSplash):
                {
                    CreateFireSplash();
                    break;
                }
                case (SkillsEnum.BetterFireBalls):
                {
                    ChangeShooterProjectile();
                    break;
                }
                case (SkillsEnum.HellFire):
                {
                    LaunchHellFire();
                    break;
                }
            }
            
        }
        
        private void DeactivateSkill(SkillsEnum skill)
        {
            if (skill==SkillsEnum.None)
            {
                for (int i = 0; i <= _skillsManaCost.Length; i++)
                {
                    DeactivateSkill((SkillsEnum)i);
                }
            }
            switch (skill)
            {
                case (SkillsEnum.FireSplash):
                    break;
                case (SkillsEnum.BetterFireBalls):
                {
                    StopCoroutine(_changeProjectileCoroutine);
                    _animationPlayer.StopBetterAttackingAnimation();
                    _fxContrlEvent.PauseCertainFX(2);
                    break;
                }
                case (SkillsEnum.HellFire):
                {
                    StopCoroutine(_hellFireCoroutine);
                    break;
                }
            }
            
            
            
            
        }
       
        public void CreateFireSplash()
        {
            _shooter.ShootBullet(_fireSplashProjectile,transform);
        }

        public void ChangeShooterProjectile()
        {
            
            if (_changeProjectileCoroutine != null)
            {
                StopCoroutine(_changeProjectileCoroutine);
                _isBetterFireBallInProgress = false;
            }
            _changeProjectileCoroutine = StartCoroutine(ChangeProjectileForATime());
            
        }

        public void LaunchHellFire()
        {
            if (_hellFireCoroutine != null)
            {
                StopCoroutine(_hellFireCoroutine);
                _isHellFireInProgress = false;
            }
            _hellFireCoroutine = StartCoroutine(LaunchHellFireCoroutine());
            
        }
        

        IEnumerator ChangeProjectileForATime()
        {
            _isBetterFireBallInProgress = true;
            _animationPlayer.StartBetterAttackingAnimation();
            _fxContrlEvent.PlayCertainFX(2);
            var standardFireBall = _shooter._bullet;
            _shooter._bullet = _betterFireballProjectile;
            yield return new WaitForSeconds(_skillsDuration[(int)SkillsEnum.BetterFireBalls]);
            _shooter._bullet = standardFireBall;
            _isBetterFireBallInProgress = false;
            _animationPlayer.StopBetterAttackingAnimation();
            _fxContrlEvent.PauseCertainFX(2);
        }

        IEnumerator LaunchHellFireCoroutine()
        {
            _isHellFireInProgress = true;
            var startTime = 0f;
            while (startTime <= _skillsDuration[(int)SkillsEnum.HellFire])
            {
                _hellFireBallSpawner.ShootHellFireBallFromRandomPosition();
                yield return new WaitForSeconds(Random.Range(0, 1f));
                _hellFireBallSpawner.ShootHellFireBallFromRandomPosition();
                yield return new WaitForSeconds(Random.Range(0.2f, 0.8f));
                _hellFireBallSpawner.ShootHellFireBallFromRandomPosition();
                yield return new WaitForSeconds(Random.Range(0f, 0.3f));
                _hellFireBallSpawner.ShootHellFireBallFromRandomPosition();
                yield return new WaitForSeconds(Random.Range(0f, 0.5f));
                startTime += 1.4f;
            }
            _isHellFireInProgress = false;
        }


    }
}
