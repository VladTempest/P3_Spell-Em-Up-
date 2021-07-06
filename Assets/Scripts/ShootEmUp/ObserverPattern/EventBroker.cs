using System;
using ShootEmUp.Sounds;
using UnityEngine;

namespace ShootEmUp.ObserverPattern
{
    public static class EventBroker
    {
        #region Actions/Gameplay
     public static event Action EnemyDied;
       public static event Action PlayerDied;
       public static event Action PlayerWon;
       public static event Action PlayerSpawned;
       public static event Action SkillUsedByPlayer;
       public static event Action RestartButtonClicked;

        #endregion Actions/Gameplay
        
        #region Actions/MainMenu
        public static event Action PlayButtonClicked;
        #endregion Actions/MainMenu
        
        
        #region Calls/Gameplay
        
        public static void CallPlayerSpawned()
        {
            PlayerSpawned?.Invoke();
        } 
        public static void CallEnemyDied()
        {
            EnemyDied?.Invoke();
        }
        
        public static void CallPlayerDied()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_GameOver_SFX);
            PlayerDied?.Invoke();
        }
        
        public static void CallPlayerWon()
        {
            SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_Win_SFX);
            PlayerWon?.Invoke();
        }
        
        public static void CallSkillUsedByPlayer()
        {
            SkillUsedByPlayer?.Invoke();
        }

        public static void CallRestartButtonClicked()
        {
            RestartButtonClicked?.Invoke();
        }

        #endregion Calls/Gameplay
        
        #region Calls/MainMenu
        public static void CallPlayButtonClicked()
        {
            PlayButtonClicked?.Invoke();
        }
        #endregion Calls/MainMenu
    }
}
