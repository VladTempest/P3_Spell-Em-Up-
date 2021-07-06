using System;
using ShootEmUp.Animation;
using ShootEmUp.Characteristics;
using ShootEmUp.Managers;
using ShootEmUp.Shooters;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.EnemyAttacker
{
    public class BomberEnemyAttacker : EnemyAttacker
    {
        [SerializeField]
        private GameObject _bombPrefab;
        [SerializeField]
        private EnemyCharacteristics _enemyCharacteristics;

        private void Start()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.Bomber_Lights,transformOfPlayPoint:transform);
        }
        private void SelfDestroy()
        {
            var transformCash = transform;
            Instantiate(_bombPrefab, transformCash.position, transformCash.rotation);
        }
        private void OnDestroy()
        {
            if (_enemyCharacteristics.HealthPoints <= 0)
            {
                SelfDestroy();
            }
        }
    }
}
