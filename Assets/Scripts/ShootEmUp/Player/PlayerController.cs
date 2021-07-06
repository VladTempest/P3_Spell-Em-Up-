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
                    _stickOffset = attackOffset;
                }
                else if (moveOffset.magnitude!=0)
                {
                    _stickOffset = moveOffset;
                }
                else
                {
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
            
            float angleForPlayerRotation = Mathf.Atan2(lookDirection.y, lookDirection.x)*Mathf.Rad2Deg-90f;
            _rigidbody2D.rotation = angleForPlayerRotation;
            
        }

        
    }
}
