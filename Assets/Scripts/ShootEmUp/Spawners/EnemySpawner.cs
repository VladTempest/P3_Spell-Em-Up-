using System;
using System.Collections;
using System.Collections.Generic;
using ShootEmUp.Characteristics;
using ShootEmUp.GameConfigs;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Settings;
using ShootEmUp.Settings.EnemiesSettings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ShootEmUp.Spawners
{
    public class EnemySpawner : MonoBehaviour,ISubscribers
    {
      
        
        public SpawnersFormType spawnersFormType=SpawnersFormType.CirclePerimeter; //Если буду менять данное поле - изменить его в расширении редактора для данного скрипта
        public float spawnRadius = 5f;
        public float spawnHeight = 5f;
        public float spawnWidth = 5f;
        [SerializeField]
        private bool _isSpawnInVolume = false;

        public SpawnersCycleType spawnersCycleType = SpawnersCycleType.Infinite;
        public int numberOfCycles = 3;

        public SpawnersTriggerType spawnersTriggerType = SpawnersTriggerType.Timer;
        public Collider2D triggerCollider = null;
        public bool isTriggerColliderReActivatable = true;
        public float pauseBeforeSpawn = 0;
        public KeyCode keyCodeForSpawn = KeyCode.F;

        [SerializeField] private bool _isSpawnerActive = true;   
        [SerializeField] private EnemiesListSettings _enemiesListSettings = null;
        
        [SerializeField] private float _spawnRate =1.2f;
        [SerializeField] private bool _isPickingRandom = true;
        private int _currentEnemie = -1;
        
        private Coroutine _spawnCoroutine=null;
        private void Awake()
        {
            //_spawnRate = GameManager.Instance.configsDictionary[ConfigsEnum.Configs.EnemiesSpawnRate];
            if (spawnersTriggerType == SpawnersTriggerType.TriggerCollider)
            {
                triggerCollider = GetComponent<Collider2D>();
            }
            
            if (spawnersCycleType == SpawnersCycleType.Infinite)
            {
                numberOfCycles = 2147483646;
            }

            if (spawnersTriggerType != SpawnersTriggerType.Timer)
            {
                pauseBeforeSpawn = 0;
            }
            Subscribe();
        }

        private void Start()
        {
            
            if (spawnersTriggerType == SpawnersTriggerType.Timer)
            {
                _spawnCoroutine= StartCoroutine(SpawnEnemies());
            }
        }



        private void InstantiateEnemy(bool isPickingRandom)
        {
            int listLength = _enemiesListSettings.list.Count ;
            
            if (isPickingRandom)
            {
                _currentEnemie = Random.Range(0, listLength);
            }
            else
            {
                _currentEnemie++;
                if (_currentEnemie == listLength)
                {
                    _currentEnemie = 0;
                }
            }
            
            Instantiate(_enemiesListSettings.list[_currentEnemie],ReturnPosition() , transform.rotation);
        }
        private Vector3 ReturnPosition()
        {
            Vector3 position=new Vector3();
            var transformPosition = transform.position;
            switch (spawnersFormType)
            {
                case (SpawnersFormType.CirclePerimeter):
                    position = RandomCircle(transformPosition, spawnRadius);
                    break;
                case(SpawnersFormType.SquarePerimeter):
                    position = RandomSquare(transformPosition,spawnHeight,spawnWidth);
                    break;
            }
            
            if (_isSpawnInVolume)
            {
                position = new Vector3(Random.Range(position.x, transformPosition.x), Random.Range(position.y, transformPosition.y), 0);
            }
            return position;
        }

        IEnumerator SpawnEnemies()
        {
            if (!_isSpawnerActive) yield break;
            var numberOfInstantiatedEnemies = 0;
            
            yield return new WaitForSeconds(pauseBeforeSpawn);
            while (numberOfInstantiatedEnemies<numberOfCycles)
            {
                
                InstantiateEnemy(_isPickingRandom);
                yield return new WaitForSeconds(_spawnRate);
                numberOfInstantiatedEnemies++;
            }
            
        }

        void StopSpawnin()
        {
            if (_spawnCoroutine!=null)
                StopCoroutine(_spawnCoroutine);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((spawnersTriggerType == SpawnersTriggerType.TriggerCollider)&&(other.gameObject.GetComponent<PlayerCharacterstics>()!=null))
            {
                _spawnCoroutine= StartCoroutine(SpawnEnemies());
                if (!isTriggerColliderReActivatable)
                {
                    triggerCollider.enabled = false;
                }
            }
        }
        void Update()
        {
            if ((spawnersTriggerType==SpawnersTriggerType.Button)&&Input.GetKeyDown(keyCodeForSpawn))
            {
                if (_spawnCoroutine!=null){StopCoroutine(_spawnCoroutine);}
                
                _spawnCoroutine= StartCoroutine(SpawnEnemies());
            }
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


        public void ActivateDeactivateSpawn(bool wannaActivate)
        {
            _isSpawnerActive = wannaActivate;
        }
        
        
        private void OnDestroy()
        {
            Unsubscribe();
        }
        
        public void Subscribe()
        {
            EventBroker.PlayerDied += StopSpawnin;
            EventBroker.PlayerWon += StopSpawnin;
        }
        public void Unsubscribe()
        {
            EventBroker.PlayerDied -= StopSpawnin;
            EventBroker.PlayerWon -= StopSpawnin;
        }

        private void OnDrawGizmos()
        {
            switch (spawnersFormType)
            {
                case (SpawnersFormType.CirclePerimeter):
                    Gizmos.DrawWireSphere(transform.position,spawnRadius);
                    break;
                case(SpawnersFormType.SquarePerimeter):
                    Gizmos.DrawWireCube(transform.position,new Vector3(spawnHeight,spawnWidth,0.5f));
                    break;
            }
            if (_isSpawnInVolume)
            {
                Gizmos.color = new Color(1, 0, 0);
            }
            
            
        }
    }
}
