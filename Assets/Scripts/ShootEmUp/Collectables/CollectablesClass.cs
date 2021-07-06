using System;
using ShootEmUp.FX;
using UnityEngine;
namespace ShootEmUp.Collectables
{
    public class CollectablesClass : MonoBehaviour
    {
        [SerializeField]
        private CollectablesEnum _typeOfCollectables;

        [SerializeField]
        private BoxCollider2D _collectableCollider = null;

        [SerializeField]
        private FXContrlEvent _fxContrlEvent;
        

        private protected virtual void InteractWithCollector(Collider2D  collectorCollider)
        {
            //none
        }

        private protected virtual void DestroyAfterInteraction()
        {
            _fxContrlEvent.SaveParticlesWithDestroyingAfterDelay(isPauseOfFXNeeded:true);
            Destroy(gameObject);
        }
    }
}
