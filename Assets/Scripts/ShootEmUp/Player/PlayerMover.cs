using System;
using UnityEngine;
using UnityEngine.AI;

namespace ShootEmUp.Player
{
    [RequireComponent(typeof(Rigidbody2D))] [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMover : MonoBehaviour
    {
        public float moveSpeed;
        public Rigidbody2D playerRigidbody;
        private Vector2 _movement;

        [SerializeField]
        private NavMeshAgent _navMeshAgent = null;
        [SerializeField]
        private AnimationPlayer _playerAnimationPLayer = null;

        [SerializeField]
        private bool _isActivePCControls = false;

        [SerializeField]
        private Joystick _joystickMove = null;
        [SerializeField]
        private float _deadZoneOfStick = 0.2f;



        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody2D>();

            _playerAnimationPLayer = GetComponentInChildren<AnimationPlayer>();

            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _navMeshAgent.updateUpAxis = false;

        }

        private void Update()
        {
            if (_isActivePCControls)
            {
                _movement.x = Input.GetAxisRaw("Horizontal");
                _movement.y = Input.GetAxisRaw("Vertical");
            }
            else
            {
                _movement.x = _joystickMove.Horizontal;
                _movement.y = _joystickMove.Vertical;
                /*
                if ((_joystickMove.Horizontal)>_deadZoneOfStick)
                    _movement.x = 1;
                else if ((_joystickMove.Horizontal) <- _deadZoneOfStick)
                    _movement.x = -1;
                else
                    _movement.x = 0f;
                
                if ((_joystickMove.Vertical)>_deadZoneOfStick)
                    _movement.y = 1;
                else if ((_joystickMove.Vertical) <-_deadZoneOfStick)
                    _movement.y = -1;
                else
                    _movement.y = 0f;
                */
            }







        }
        void FixedUpdate()
        {

            if (_movement.magnitude != 0)
            {

                _navMeshAgent.isStopped = false;
                Vector3 differencePosition = new Vector3(_movement.x, _movement.y, 0) * (moveSpeed);
                var destination = transform.position + differencePosition;
                _navMeshAgent.SetDestination(destination);
                if (_navMeshAgent.velocity.magnitude != 0)
                {
                    _playerAnimationPLayer.StartMovingAnimation();
                }
                else
                {
                    _playerAnimationPLayer.StopMovingAnimation();
                }
            }
            if (_movement.magnitude == 0)
            {
                _navMeshAgent.isStopped = true;
                _playerAnimationPLayer.StopMovingAnimation();
            }





        }
    }
}
