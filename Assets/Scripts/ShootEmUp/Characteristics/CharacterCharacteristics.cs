using ShootEmUp.Projectile;
using ShootEmUp.UI;
using UnityEngine;

namespace ShootEmUp.Characteristics
{
    public abstract class CharacterCharacteristics : MonoBehaviour
    {
        [SerializeField]
        protected float _maxHealthPointsCoefficient = 1f;
        [SerializeField]
        private HealthBar _healthBar;

        private float _currentHealthPoints;
        protected float _maxHealth = 1f;
        protected virtual void DestroyCharacter()
        {
            Destroy(gameObject);
        }
        public float HealthPoints
        {
            get => _currentHealthPoints;
            set
            {
                _currentHealthPoints = value;
                if (_currentHealthPoints <= 0)
                {
                    DestroyCharacter();
                }
                if (_currentHealthPoints > _maxHealth)
                {
                    _currentHealthPoints = _maxHealth;
                }
                _healthBar.SetCurrentHealthValue(value);
            }
        }

        public virtual void Awake()
        {
            _healthBar = GetComponentInChildren<HealthBar>();
        }

        private void Start()
        {
            _healthBar.SetMaxHealthValue(_maxHealth);
            _healthBar.SetCurrentHealthValue(_maxHealth);
            HealthPoints = _maxHealth;
        }

        public virtual void ReduceHealth(float damageValue)
        {
            HealthPoints -= damageValue;
        }
        
        public virtual void AddHealth(float healValue)
        {
            HealthPoints += healValue;
        }

        public virtual void OnCollisionEnter2D(Collision2D other)
        {
            var bullet = other.gameObject.GetComponent<ProjectileClass>();
            if (bullet != null)
                ReduceHealth(bullet.bulletDamage);
        }

    }
}
