using System;
using System.Collections.Generic;
using UnityEngine;
namespace ShootEmUp.FX
{
    public class FXParticlesAfterDeathOfEmitterKeeper : MonoBehaviour
    {
        private List<ParticleSystem> _particleSystemList=null;
        private List<Transform> _transformList=null;
        private  float delayOfDestroy = 2f;
        [SerializeField]
        private List<GameObject> _gameObjectWithParticleSystemList=null;
        
        private void Awake()
        { 
            _particleSystemList = new List<ParticleSystem>();
            foreach(GameObject gO in _gameObjectWithParticleSystemList)
            {
                
                _particleSystemList.Add(gO.GetComponent<ParticleSystem>());
                _transformList.Add(gO.GetComponent<Transform>());
            }
        }
        private void StopEmmitng()
        {
            for (int i=0; i<_gameObjectWithParticleSystemList.Count;i++)
            {
                var emissionModule = _particleSystemList[i].emission;
                emissionModule.enabled=false;

            }
           
        }
        }
        
}
