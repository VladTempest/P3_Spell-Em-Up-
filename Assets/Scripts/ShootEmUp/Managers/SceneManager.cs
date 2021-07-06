using System.Collections;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp.Managers
{


    public class SceneManager : SingletonBase<SceneManager>,ISubscribers
    {
        private enum Scenes
        {
            MainMenu=0, Level2_Dungeoun=1, GameplayScene=2
        }

        [SerializeField] private float _pauseBeforeLoadMainMenu=2f;
        [SerializeField] private float _pauseBeforeLoadGameplay=0.1f;
        
        private void Awake()
        {
            var sceneManager = SceneManager.Instance; //Initialize singleton pattern
            Subscribe();
            
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void LoadGameplayScene()
        {
            StartCoroutine(TimerForLoadScene(Scenes.Level2_Dungeoun,_pauseBeforeLoadGameplay)); //This is for scripts to get configs values for GameManager
            SoundtrackPlayer.Instance.PlaySoundtrack(TypeOfOSTByItsNature.Gameplay_Level1);    
        }

        private void LoadMainMenu()
        {
            
            StartCoroutine(TimerForLoadScene(Scenes.MainMenu,_pauseBeforeLoadMainMenu));
            
        }

        private void RestartScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            SoundtrackPlayer.Instance.PlaySoundtrack(TypeOfOSTByItsNature.Gameplay_Level1);
        }
        
        IEnumerator TimerForLoadScene(Scenes scene,float pauseBeforeLoad)
        {
            yield return new WaitForSeconds(pauseBeforeLoad);
            UnityEngine.SceneManagement.SceneManager.LoadScene((int)scene);
        }
        
        public void Subscribe()
        {

            EventBroker.PlayerDied += LoadMainMenu;
            EventBroker.PlayerWon += LoadMainMenu;
            EventBroker.RestartButtonClicked += RestartScene;
            
            EventBroker.PlayButtonClicked += LoadGameplayScene;
            
        }
        public void Unsubscribe()
        {
            EventBroker.PlayerDied -= LoadMainMenu;
            EventBroker.PlayerWon -= LoadMainMenu;
            EventBroker.RestartButtonClicked -= RestartScene;
            
            EventBroker.PlayButtonClicked -= LoadGameplayScene;
            
        }
    }
    
    
}
