using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.UI;
namespace ShootEmUp.UI
{
    public class PlayButton : MonoBehaviour,IClickableWithSound
    {
        void Awake()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(EventBroker.CallPlayButtonClicked);
            gameObject.GetComponent<Button>().onClick.AddListener(PlaySFXOfButton);
        }

        public void PlaySFXOfButton()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_PlayButton_SFX);
        }
        void OnDestroy()
        {
            gameObject.GetComponent<Button>().onClick.RemoveListener(EventBroker.CallPlayButtonClicked);
            gameObject.GetComponent<Button>().onClick.RemoveListener(PlaySFXOfButton);
        
        }
    }
}
