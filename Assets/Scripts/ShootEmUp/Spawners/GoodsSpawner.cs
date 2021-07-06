using System;
using ShootEmUp.Projectile;
using ShootEmUp.Settings;
using ShootEmUp.Sounds;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp.Spawners
{
    public class GoodsSpawner : MonoBehaviour
    {

        private int _listLength = 0;
        private bool _canSpawn = true;
        
        [SerializeField]
        private CollectablesListSettings _collectablesListSettings;
        [SerializeField]
        private Transform _popUpPoint;
        [SerializeField]
        private float _forceValue;
        private bool CanSpawn
        {
            get
            {
                return _canSpawn;
            }
            set
            {
                _canSpawn = value;
                if (!value)
                {
                    _animationPlayer.StartOpeningAnimation();
                }   
            }
        }

        [SerializeField]
        private AnimationPlayer _animationPlayer;
        


        private void Awake()
        {
            _animationPlayer = GetComponentInChildren<AnimationPlayer>();
            _listLength = _collectablesListSettings.list.Count;
        }
        
        public void InstantiateCollectable()
        {
            GameObject instantiatedCollectable= Instantiate(_collectablesListSettings.list[Random.Range(0, _listLength)].gameObject, transform.position, transform.rotation);
            PushFromSpawner(instantiatedCollectable);
            CanSpawn = false;
        }

        private void PushFromSpawner(GameObject spawnedObject)
        {
            var objectRigidBody =spawnedObject.GetComponent<Rigidbody2D>();
            objectRigidBody.AddForce((_popUpPoint.position-transform.position).normalized*_forceValue,ForceMode2D.Impulse);
            
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (CanSpawn)
            {
                
                var projectile = other.gameObject.GetComponent<ProjectileClass>();
                if (projectile != null)
                {
                    SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Door_Open,transformOfPlayPoint:transform);
                    InstantiateCollectable();
                }
            }
        }
        
        
    }
}
