using ShootEmUp.Characteristics;
using ShootEmUp.GameConfigs;
using ShootEmUp.Managers;
using ShootEmUp.TriggerCircles;
using UnityEngine;

namespace ShootEmUp.EnemyAttacker
{[RequireComponent(typeof(AnimationPlayer))]
    public abstract class EnemyAttacker : MonoBehaviour
    {
        protected internal float attackDamage = 1f;
        protected PlayerCharacterstics _playerCharacteristics;
        [SerializeField]
        protected float _attackDamageCoefficient = 1f;
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private Triggers.CircleTriggers _triggerToStartAttacking;

        [SerializeField]
        private protected bool _isInAttackingState = false; 
        
        [SerializeField]
        private AnimationPlayer _enemyAnimationPlayer = null;


        
        protected void Awake()
        {
            attackDamage = GameManager.Instance.configsDictionary[ConfigsEnum.Configs.EnemiesDamage] * _attackDamageCoefficient;


            _enemyAnimationPlayer = GetComponentInChildren<AnimationPlayer>();
            
            _player = GameManager.Instance.player;
            _playerCharacteristics = _player.GetComponent<PlayerCharacterstics>();
        }
        
        protected internal virtual void Attack()
        {
            _isInAttackingState = true;
            _enemyAnimationPlayer.StartAttackingAnimation();
               
        }
        
        protected internal virtual void StopAttack()
        {
            _isInAttackingState = false;
            _enemyAnimationPlayer.StopAttackingAnimation();
               
        }
        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<TriggerCircle>() != null)
                if (other.GetComponent<TriggerCircle>().typeOfTrigger == _triggerToStartAttacking)
                    Attack();
        }
        
        protected void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<TriggerCircle>() != null)
                if (other.GetComponent<TriggerCircle>().typeOfTrigger == _triggerToStartAttacking)
                    StopAttack();
        }
    }
}
