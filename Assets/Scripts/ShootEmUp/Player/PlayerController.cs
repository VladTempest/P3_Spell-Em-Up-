using System;
using ShootEmUp.Managers;
using UnityEngine;

namespace ShootEmUp.Player
{[RequireComponent(typeof(Rigidbody2D))] 
    public class PlayerController :MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private Rigidbody2D _rigidbody2D;
        [SerializeField]
        private Vector2 _mousePosition;

        [SerializeField]
        private bool _isActivePCControls = false;
        [SerializeField]
        private Joystick _joystickAttack=null;
        [SerializeField]
        private Joystick _joystickMove = null;

        [SerializeField]
        private float maxTimeOffsetAfterUsingMoveJoystick = 1f;
        private float timeOffsetAfterUsingMoveJoystick = 0f;

        [SerializeField]
        private float speedOfRotation = 0.2f;
        [SerializeField]
        private Vector2 _stickOffset;
        
        
 
        void Awake()
        { _camera=Camera.main;
        _rigidbody2D = GetComponent<Rigidbody2D>();

        GameManager.Instance.player = gameObject;
        }
        
        void Update()
        {
            if (_isActivePCControls)
            {
                _mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                var attackOffset = new Vector2(_joystickAttack.Horizontal, _joystickAttack.Vertical);
                var moveOffset = new Vector2(_joystickMove.Horizontal, _joystickMove.Vertical);

                if (attackOffset.magnitude != 0)
                {
                    timeOffsetAfterUsingMoveJoystick = 0;
                    _stickOffset = attackOffset;
                }
                else if (moveOffset.magnitude!=0)
                {
                    timeOffsetAfterUsingMoveJoystick += Time.deltaTime;
                    if (timeOffsetAfterUsingMoveJoystick >= maxTimeOffsetAfterUsingMoveJoystick)
                    {
                        _stickOffset = moveOffset;
                    }
                    
                }
                else
                {
                    timeOffsetAfterUsingMoveJoystick = 0;
                    _stickOffset=Vector2.zero;
                }



            }
        }

        private void FixedUpdate()
        {
            if ((!_isActivePCControls)&&(_stickOffset.magnitude != 0))
            {
                FaceMousePointer();
            }
            if (_isActivePCControls)
            {
                FaceMousePointer();
            }
               
        }

        private void FaceMousePointer()
        {
            Vector2 lookDirection;
            if (_isActivePCControls)
            {
                lookDirection= _mousePosition - _rigidbody2D.position;
            }
            else
            {
                lookDirection = _stickOffset;
            }
            
            
            var lookRotation = Quaternion.LookRotation(Vector3.forward,lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation,  speedOfRotation);

        }

        
    }
}
