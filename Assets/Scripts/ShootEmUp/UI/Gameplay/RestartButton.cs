using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI.Gameplay
{
    public class RestartButton : MonoBehaviour,IClickableWithSound
    {
        
            void Awake()
            {
               GetComponent<Button>().onClick.AddListener(EventBroker.CallRestartButtonClicked);
               GetComponent<Button>().onClick.AddListener(PlaySFXOfButton);
            }
            void OnDestroy()
            {
                GetComponent<Button>().onClick.RemoveListener(EventBroker.CallRestartButtonClicked);
                GetComponent<Button>().onClick.RemoveListener(PlaySFXOfButton);
        
            }

            public void PlaySFXOfButton()
            {
                SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_RestartButton_SFX);
            }
    }
}
