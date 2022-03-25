using System.Collections;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShootEmUp.Managers
{


    public class SceneManager : SingletonBase<SceneManager>,ISubscribers
    {
        public enum Scenes
        {
            MainMenu=0, Level2_Dungeoun=1, Level5_Arena=2
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

        private void LoadSceneLoadScene(Scenes scene)
        {
            StartCoroutine(TimerForLoadScene(scene,_pauseBeforeLoadGameplay)); //This is for scripts to get configs values for GameManager
            ChoseOstOfScene(scene);
        }
        private static void ChoseOstOfScene(Scenes scene)
        {

            TypeOfOSTByItsNature typeOfOST;
            switch (scene)
            {
                case Scenes.Level2_Dungeoun:
                {
                    typeOfOST = TypeOfOSTByItsNature.Gameplay_Level1;
                    break;
                }
                case Scenes.Level5_Arena:
                {
                    typeOfOST = TypeOfOSTByItsNature.Arena;
                    break;
                }
                default:
                    typeOfOST = TypeOfOSTByItsNature.None;
                    break;
            }
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfOST);
        }

        private void LoadMainMenu()
        {
            
            StartCoroutine(TimerForLoadScene(Scenes.MainMenu,_pauseBeforeLoadMainMenu));
            
        }

        private void RestartScene()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            //SoundtrackPlayer.Instance
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
            
            EventBroker.SceneLoadButtonClicked += LoadSceneLoadScene;
            
        }
        public void Unsubscribe()
        {
            EventBroker.PlayerDied -= LoadMainMenu;
            EventBroker.PlayerWon -= LoadMainMenu;
            EventBroker.RestartButtonClicked -= RestartScene;
            
            EventBroker.SceneLoadButtonClicked -= LoadSceneLoadScene;
            
        }
    }
    
    
}
