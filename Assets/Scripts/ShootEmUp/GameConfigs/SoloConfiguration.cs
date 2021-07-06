using System;
using System.Globalization;
using ShootEmUp.ObserverPattern;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.GameConfigs
{
    public class SoloConfiguration:MonoBehaviour
    {
        
        public ConfigsEnum.Configs typeOfConfig=0;
        [SerializeField]
        private float _defaultValue;
        [SerializeField]
        private TMP_InputField _inputField;
        private float _currentValue;
        public float CurrentValue
        {
            get
            {
                _currentValue = _inputField.text == "" ? _defaultValue : float.Parse(_inputField.text); //Check if InputField is empty
                return _currentValue;
            }
        }
        private void Awake()
        {
            _inputField = GetComponentInChildren<TMP_InputField>();
            _inputField.text = _defaultValue.ToString(CultureInfo.CurrentCulture);
        }

        
    }
}
