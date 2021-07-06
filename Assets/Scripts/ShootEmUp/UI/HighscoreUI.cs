
using ShootEmUp.Managers;
using TMPro;
using UnityEngine;
namespace ShootEmUp.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HighscoreUI : MonoBehaviour
    {

        private TextMeshProUGUI _highscoreText = null;
        
        private void Awake()
        {
            GameManager.Instance.highscoreUI = this;
            _highscoreText = GetComponent<TextMeshProUGUI>();
            GameManager.Instance.ResetCurrentHighscore();
        }

        public void ChangeHighscore(string newHighscore)
        {
            _highscoreText.SetText(newHighscore);
        }
    }
}
