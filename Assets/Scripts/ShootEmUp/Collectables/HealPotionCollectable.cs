using System;
using ShootEmUp.Characteristics;
using ShootEmUp.Player;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.Collectables
{
    public class HealPotionCollectable : CollectablesClass
    {
        [SerializeField]
        private float _healValue = 10f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            InteractWithCollector(other);
            
        }

        private void Start()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.HealPotion_Drop,transformOfPlayPoint:transform);
        }
        private protected override void InteractWithCollector(Collider2D  collectorCollider)
        {
            
            var playerCharacteristics = collectorCollider.gameObject.GetComponent<PlayerCharacterstics>();
            if (playerCharacteristics!= null)
            {
                SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.HealPotion_Use,transformOfPlayPoint:transform);
                playerCharacteristics.AddHealth(_healValue);
                DestroyAfterInteraction();
            } 
        }
    }
}
