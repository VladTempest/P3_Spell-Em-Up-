using System;
using System.Collections.Generic;
using ShootEmUp.Managers;
using ShootEmUp.ObserverPattern;
using UnityEngine;
using UnityEngine.UI;
namespace ShootEmUp.UI.SkillsUI
{
    public class SkillManaBar : MonoBehaviour,ISubscribers
    {
        [SerializeField]
        private SkillIconUI[] _skillIcon;
        [SerializeField]
        private Slider _manaSlider = null;
        [SerializeField]
        private Dictionary<float,SkillIconUI> _manaCostForSkill;




        private void Awake()
        {
            _manaSlider = GetComponentInChildren<Slider>();
            GameManager.Instance.ResetCurrentMana();
            Subscribe();
        }

        private void Start()
        {
            PrepareManaSlider();
            CreateDictionary();
        }

        private void PrepareManaSlider()
        {
            _manaSlider.maxValue = GameManager.Instance._maxMana;
            SetCurrentManaValueOnSlider();
        }
        private void SetCurrentManaValueOnSlider()
        {
            var currentMana = GameManager.Instance.CurrentMana;
            _manaSlider.value = currentMana;
        }
        private void CreateDictionary()
        {
            _manaCostForSkill = new Dictionary<float,SkillIconUI>();
            foreach (SkillIconUI skill in _skillIcon)
            {
                _manaCostForSkill.Add(skill._skillManaCost,skill);
            }
            
        }

        private void CheckIfEnoughManaForSkills()
        {
            foreach (SkillIconUI skill in _skillIcon)
            {
                if (GameManager.Instance.CurrentMana < skill._skillManaCost)
                {
                    skill.MakeInteractive(false);
                }
            }
            SetCurrentManaValueOnSlider();
        }
        private void CheckMana()
        {
            var currentMana = GameManager.Instance.CurrentMana;
            SetCurrentManaValueOnSlider();
            if (_manaCostForSkill.ContainsKey(currentMana))
            {
                _manaCostForSkill[currentMana].MakeInteractive(true);
            }
            

        }
        public void Subscribe()
        {
            EventBroker.EnemyDied += CheckMana;
            EventBroker.SkillUsedByPlayer += CheckIfEnoughManaForSkills;
        }
        public void Unsubscribe()
        {
            EventBroker.EnemyDied -= CheckMana;
            EventBroker.SkillUsedByPlayer -= CheckIfEnoughManaForSkills;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
