using System;
using System.Collections.Generic;
using ShootEmUp.GameConfigs;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Sounds;
using ShootEmUp.UI;
using UnityEngine;

namespace ShootEmUp.Managers
{
    public class GameManager : SingletonBase<GameManager>,ISubscribers
    {   
        
        [SerializeField]
        private float _currentMana = 0f;
        
        private float CurrentHighscore
             {
                 get => _currentHighscore;
                 set
                 {
                     _currentHighscore = value;
                     highscoreUI.ChangeHighscore(_currentHighscore+"/"+maxScore);
                     if (_currentHighscore >= maxScore)
                     {
                         EventBroker.CallPlayerWon();
                     }
                 }
             }
        public Dictionary<ConfigsEnum.Configs,float> configsDictionary=new Dictionary<ConfigsEnum.Configs, float>();
        public HighscoreUI highscoreUI = null;
        public GameObject player;
        public float enemySpeed = 10f;
        public float maxScore = 50f;
        public float _maxMana = 20f;
        public float CurrentMana
        {
            get => _currentMana;
            set
            {
                if (value > _maxMana||value<0) return;
                _currentMana = value;
            }
        }
        [SerializeField]
        private float _currentHighscore = 0f;
        [SerializeField]
        private float _scoreForEnemyKill = 1f;
        public bool isGameActive = false;
        

        public void Awake()
       {
           Subscribe();
           
       }

        public void Start()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfOstByItsNature:TypeOfOSTByItsNature.MainMenu);
        }
        public void OnDestroy()
        {
            Unsubscribe();
        }


        private void AddManaForEnemyKill()
        {
            CurrentMana++;
        }

        public bool CheckIfThereIsEnoughManaForSkill(float manaCost)
        {
            return CurrentMana >= manaCost;
        }
        public void UseManaForSkill(float manaCost)
        {
            CurrentMana -= manaCost;
        }
        private void AddScoreForEnemyKill()
        {
            CurrentHighscore += _scoreForEnemyKill;
        }
        public void ResetCurrentHighscore()
        {
            CurrentHighscore = 0;
        }
        public void ResetCurrentMana()
        {
            CurrentMana = 0;
        }
        

        
        public void AssignConfigValues(SoloConfiguration[] configsArray )
        {
            foreach (SoloConfiguration soloConfig in configsArray)
            {
                if (configsDictionary.ContainsKey(soloConfig.typeOfConfig))
                {
                    configsDictionary[soloConfig.typeOfConfig] = soloConfig.CurrentValue;
                    return;
                }
                configsDictionary.Add(soloConfig.typeOfConfig, soloConfig.CurrentValue);
            }
        }

        private void StartActiveStateOfGame()
        {
            isGameActive= true;
        }
        
        private void StopActiveStateOfGame()
        {
            isGameActive= false;
        }
        
        public void Subscribe()
        {
            EventBroker.EnemyDied+=AddScoreForEnemyKill;
            EventBroker.EnemyDied+=AddManaForEnemyKill;
            EventBroker.PlayerDied += ResetCurrentHighscore;
            EventBroker.PlayerWon += ResetCurrentHighscore;
            EventBroker.PlayerDied += ResetCurrentMana;
            EventBroker.PlayerWon += ResetCurrentMana;
            EventBroker.PlayerDied += StopActiveStateOfGame;
            EventBroker.PlayerWon += StopActiveStateOfGame;
            EventBroker.PlayerSpawned += StartActiveStateOfGame;
        }
        public void Unsubscribe()
        {
            EventBroker.EnemyDied-=AddScoreForEnemyKill;
            EventBroker.EnemyDied-=AddManaForEnemyKill;
            EventBroker.PlayerDied -= ResetCurrentHighscore;
            EventBroker.PlayerWon -= ResetCurrentHighscore;
            EventBroker.PlayerDied -= ResetCurrentMana;
            EventBroker.PlayerWon -= ResetCurrentMana;
            EventBroker.PlayerDied -= StopActiveStateOfGame;
            EventBroker.PlayerWon -= StopActiveStateOfGame;
            EventBroker.PlayerSpawned -= StartActiveStateOfGame;
        }
    }
}
