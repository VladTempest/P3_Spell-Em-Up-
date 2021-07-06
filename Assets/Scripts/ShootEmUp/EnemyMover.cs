using System;
using ShootEmUp.Managers;
using ShootEmUp.Player;
using ShootEmUp.TriggerCircles;
using UnityEngine;
using UnityEngine.AI;


namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMover : MonoBehaviour
    {
        private float _enemySpeed = 1f;
        private Rigidbody2D _enemyRigidbody = null;
        [SerializeField]
        private float _enemySpeedCoefficient = 1f;
        [SerializeField]
        private Transform _playerTransform = null;
        [SerializeField]
        private Triggers.CircleTriggers _typeOfCircleTriggerToStop;
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        private bool _isMovingState = true;
        
        [SerializeField]
        private Transform _leftEyeEdge;
        [SerializeField]
        private Transform _rightEyeEdge;
        [SerializeField]
        private EnemyAttacker.EnemyAttacker _enemyAttacker;
        
        
        [SerializeField]
        private NavMeshAgent _navMeshAgent = null;
        void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _enemySpeed = GameManager.Instance.enemySpeed * _enemySpeedCoefficient;
            
            _navMeshAgent.speed = _enemySpeed;
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;
            
            
            _player = GameManager.Instance.player;
           
            _enemyRigidbody = GetComponent<Rigidbody2D>();
            _playerTransform = _player.GetComponent<Transform>();
            FacePlayer();
            MoveCharacter();

        }

        private void LateUpdate()
        {

            if (_typeOfCircleTriggerToStop == Triggers.CircleTriggers.RangedCombat)
            {
                if (_isMovingState&&!CheckIfPlayerInSightZone())
                {
                    _enemyAttacker.StopAttack();
                    MoveCharacter();
                }
                if (_isMovingState&&CheckIfPlayerInSightZone())
                {
                    _enemyAttacker.Attack();
                    StopCharacter();
                }
                if (!_isMovingState&&!CheckIfPlayerInSightZone())
                {
                    _enemyAttacker.StopAttack();
                    MoveCharacter();
                }
            }
            else
            {
                if (_isMovingState)
                {
                    MoveCharacter();
                } 
            }
            
            
            
            
            
           
            FacePlayer();
        }
        void MoveCharacter()
        {
            if (_navMeshAgent.isOnNavMesh)
            {
                _isMovingState = true;
                _navMeshAgent.isStopped = false;
                _navMeshAgent.SetDestination(_playerTransform.position);
            }


        }

        void StopCharacter()
        {
            
                _navMeshAgent.isStopped = true;
                _isMovingState = false;
            
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<TriggerCircle>() != null)
            {
                if (other.GetComponent<TriggerCircle>().typeOfTrigger == _typeOfCircleTriggerToStop)
                {
                    StopCharacter();
                }
            }
        
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<TriggerCircle>() != null)
            {
                if (other.GetComponent<TriggerCircle>().typeOfTrigger == _typeOfCircleTriggerToStop)
                {
                    MoveCharacter();
                }
            }
        
        }

        private void FacePlayer()
        {
            Vector2 lookDirection = _playerTransform.position - transform.position;
            float angleForEnemyRotation = Mathf.Atan2(lookDirection.y, lookDirection.x)*Mathf.Rad2Deg-90f;
            _enemyRigidbody.rotation =angleForEnemyRotation;
        }

        private bool CheckIfPlayerInLine(Transform startPoint)
        {
            
            
            RaycastHit2D hit = Physics2D.Raycast(startPoint.position, _playerTransform.position-startPoint.position,7.7f);
            if (!hit)
                return false;
            return hit.transform ==_playerTransform;
        }

        private bool CheckIfPlayerInSightZone()
        {
            return CheckIfPlayerInLine(_leftEyeEdge) && (CheckIfPlayerInLine(_rightEyeEdge));
        }




    }
}
