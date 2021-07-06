using System;
using UnityEngine;

namespace ShootEmUp.FX
{
    public enum InWorldFXs
    {
       BodyExplosion, FireBallExplosion, BloodBurst 
    } 
    
    public class InWorldFXClass:MonoBehaviour
    {
        public GameObject FXGameObject = null;
        public InWorldFXs FXName;

        private float delayOfDestroy = 5f;

        private void OnEnable()
        {
            Destroy(gameObject,delayOfDestroy);
        }

    }
    
    
}
