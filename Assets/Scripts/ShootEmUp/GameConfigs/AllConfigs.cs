using System;
using System.Collections;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using UnityEngine;

namespace ShootEmUp.GameConfigs
{
    public class AllConfigs : MonoBehaviour, ISubscribers
    {
        [SerializeField]
        private SoloConfiguration[] _configsArray = null;
        private void Awake()
        {
            _configsArray = GetComponentsInChildren<SoloConfiguration>();
            Subscribe();
        }
        private void OnDestroy()
        {
            Unsubscribe();
        }
        private void PushInputtedValuesIntoGameManager()
        {
            foreach (SoloConfiguration soloConfig in _configsArray)
            {
                //Debug.Log(_configsArray[(int)soloConfig.typeOfConfig].CurrentValue);
            }
            GameManager.Instance.AssignConfigValues(_configsArray);
        }
        public void Subscribe()
        {
            EventBroker.PlayButtonClicked += PushInputtedValuesIntoGameManager;
        }
        public void Unsubscribe()
        {
            EventBroker.PlayButtonClicked -= PushInputtedValuesIntoGameManager;
        }
    }
}
