using System;
using ShootEmUp.Player;
using ShootEmUp.Shooters;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp.Spawners
{
    public class HellFireBallSpawner : MonoBehaviour
    {
        
        
        
        public float spawnHeight = 5f;
        public float spawnWidth = 5f;

        private Vector2 _transformCenter;

        [SerializeField]
        private GameObject _hellFirePrefab;
        [SerializeField]
        private Shooter _shooter;
        [SerializeField]
        private GameObject _firePoint;

        [SerializeField]
        private PlayerController _playerController;
        

        private void Awake()
        {
           
            
        }

        public void ShootHellFireBallFromRandomPosition()
        {
            ResetPositionAccordingToPlayer();
            var randomPositionOnSquare = RandomSquare(_transformCenter,  spawnHeight,spawnHeight);
            Vector3 position;
            position = new Vector3(randomPositionOnSquare.x, randomPositionOnSquare.y,0);
            _firePoint.transform.position = position;
            var hellFireBall=_shooter.ShootBullet(_hellFirePrefab, _firePoint.transform);
            Vector2 direction = _transformCenter - new Vector2(position.x, position.y);
            
            if (hellFireBall.GetComponent<Rigidbody2D>() == null) return; 
            Rigidbody2D bulletRigidBody = hellFireBall.GetComponent<Rigidbody2D>();
            bulletRigidBody.AddForce(direction.normalized * (_shooter._bulletForce/2), ForceMode2D.Impulse);
        }
        private void ResetPositionAccordingToPlayer()
        {
            transform.position = _playerController.transform.position;
            var transformPosition = transform.position;
            _transformCenter = new Vector2(transformPosition.x, transformPosition.y);
        }

        private Vector2 RandomCircle(Vector2 center, float radius)
        {
            float ang = Random.value * 360;
            Vector2 position;
            position.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            position.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return position;
        }
        
        private Vector2 RandomSquare(Vector2 center, float height,float width)
        {
            float radius = (float)Math.Sqrt(height * height + width * width) / 2;
            var transformPosition = transform.position;
            Vector2 position=RandomCircle(transformPosition,radius);
            position.x = Mathf.Clamp(position.x,  transformPosition.x - height / 2,transformPosition.x + height / 2);
            position.y = Mathf.Clamp(position.y, transformPosition.y - width / 2, transformPosition.y + width / 2);
            
            return position;
        }
        
        private void OnDrawGizmos()
        { Gizmos.color = new Color(0, 0, 1f);
           Gizmos.DrawWireCube(transform.position,new Vector3(spawnHeight,spawnWidth,0.5f));
           
        }
    }
}
