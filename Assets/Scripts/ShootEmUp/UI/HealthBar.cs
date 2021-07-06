using UnityEngine;
using UnityEngine.UI;
namespace ShootEmUp.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _slider = null;

        
            private void Awake()
            {
                _slider = GetComponent<Slider>();
            }

            public void SetCurrentHealthValue(float currentHealthValue)
            {
                _slider.value = currentHealthValue;
            }

            public void SetMaxHealthValue(float maxHealthValue)
            {
                _slider.maxValue = maxHealthValue;
            }

    }
}
