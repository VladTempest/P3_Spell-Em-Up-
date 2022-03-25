using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.UI;
namespace ShootEmUp.UI
{
    public class PlayButton : MonoBehaviour,IClickableWithSound
    {
        [SerializeField]
        private SceneManager.Scenes _sceneToLoad;
        void Awake()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(() => { EventBroker.CallSceneLoadButtonClicked(_sceneToLoad);});
            gameObject.GetComponent<Button>().onClick.AddListener(PlaySFXOfButton);
        }

        public void PlaySFXOfButton()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_PlayButton_SFX);
        }
        void OnDestroy()
        {
            gameObject.GetComponent<Button>().onClick.RemoveListener(() => { EventBroker.CallSceneLoadButtonClicked(_sceneToLoad);});
            gameObject.GetComponent<Button>().onClick.RemoveListener(PlaySFXOfButton);
        
        }
    }
}
