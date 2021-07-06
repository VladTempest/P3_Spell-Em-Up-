using System;
using System.Collections;
using System.Globalization;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using ShootEmUp.Skills;
using ShootEmUp.Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShootEmUp.UI.SkillsUI
{
    public class SkillIconUI : MonoBehaviour
    {
        private Coroutine _timerCoroutine;
        
        [SerializeField]
        private SkillsEnum _skillsEnum;
        [SerializeField]
        private Button _button;
        [SerializeField]
        private TextMeshProUGUI _timerTextMeshPro;
        [SerializeField]
        private SkillsHandler _skillsHandler;
        [SerializeField]
        private float _skillDuration;
        public float _skillManaCost;
        public float _skillCooldown;

        public KeyCode _keyToActivate;
        public bool isInteractable;

        private void Awake()
        {
            _button = GetComponent<Button>();
           // _textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            
            _button.onClick.AddListener(UseSkill);

            
            isInteractable=_button.interactable;
            MakeInteractive(false);
            GetParameters(_skillsEnum);
        }

        private void Update()
        {
            if (Input.GetKeyDown(_keyToActivate))
            {
                UseSkill();
            }
        }
        private void GetParameters(SkillsEnum skill)
        {
            _skillDuration=_skillsHandler._skillsDuration[(int)skill];
            _skillManaCost=_skillsHandler._skillsManaCost[(int)skill];
        }

        public void MakeInteractive(bool tryingToMakeInteractive)
        {
           // isInteractable = true;
            _button.interactable = tryingToMakeInteractive;
        }

        private void ShowAndLaunchTimer()
        {
            if (_skillDuration==0) return;
            _timerTextMeshPro.enabled=true;
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
            _timerCoroutine=StartCoroutine(StartTimer());

        }

        IEnumerator StartTimer()
        {
            var timeLeft = _skillDuration;
            while (timeLeft > 0)
            {
                _timerTextMeshPro.SetText(timeLeft.ToString("F1",CultureInfo.InvariantCulture));
                timeLeft -= 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
            _timerTextMeshPro.enabled=false;
        }

        private void UseSkill()
        {
            if (GameManager.Instance.CheckIfThereIsEnoughManaForSkill(_skillManaCost))
            {
                SoundtrackPlayer.Instance.PlaySoundtrack(typeOfSfxByItsNature:TypeOfSFXByItsNature.UI_MagicSkill);
                _skillsHandler.ActivateSkill(_skillsEnum);
                GameManager.Instance.UseManaForSkill(_skillManaCost);
                ShowAndLaunchTimer();
                EventBroker.CallSkillUsedByPlayer();
            }
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(UseSkill);
        }

    }
}
