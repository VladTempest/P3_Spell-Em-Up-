using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp.FX
{
    public class FXContrlEvent : MonoBehaviour
    {
        [SerializeField]
        public List<ParticleSystem> _fxObject = null;

        private float delayOfDestroy = 5f;

        public void PauseCertainFX(int numberOfFX=-1)
        {
            if (_fxObject == null) return;
            if (numberOfFX == -1)
            {
                for (int i=0;i<_fxObject.Count;i++)
                {
                    PauseCertainFX(i);
                }
                return;
            }
            
            var emission = _fxObject[numberOfFX].emission;
            emission.enabled = false;
            
        }
        
        public void PlayCertainFX(int numberOfFX=-1)
        {
            if (_fxObject == null) return;
            if (numberOfFX == -1)
            {
                for (int i=0;i<_fxObject.Count;i++)
                {
                    PlayCertainFX(i);
                }
                return;
            }
                var emission = _fxObject[numberOfFX].emission;
                emission.enabled = true;
            
        }

        public void SaveParticlesWithDestroyingAfterDelay(int numberOfFX=-1, bool isPauseOfFXNeeded=true)
        {
            if (numberOfFX == -1)
            {
                for (int i=0;i<_fxObject.Count;i++)
                {
                    SaveParticlesWithDestroyingAfterDelay(i,isPauseOfFXNeeded);
                }
                return;
            }
            
            if (_fxObject!=null)
            {
                if (isPauseOfFXNeeded)
                {
                    PauseCertainFX(numberOfFX);
                }
                _fxObject[numberOfFX].transform.parent = null;
                var transformPositionOfFx = _fxObject[numberOfFX].transform.position;
                _fxObject[numberOfFX].transform.position =new Vector3(transformPositionOfFx.x,transformPositionOfFx.y,0);
                Destroy(_fxObject[numberOfFX].gameObject, delayOfDestroy);
            }
        }




    }
}
