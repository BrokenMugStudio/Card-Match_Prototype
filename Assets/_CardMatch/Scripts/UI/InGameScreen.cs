using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts.UI
{
    public class InGameScreen : ScreenBase
    {
        [SerializeField]
        private Button m_ResetButton;
        [SerializeField]
        private TextMeshProUGUI m_ScoreText;
        [SerializeField]
        private TextMeshProUGUI m_HighScoreText;
        [SerializeField]
        private TextMeshProUGUI m_ComboText;
        private void OnEnable()
        {
            m_ResetButton.onClick.AddListener(ResetGame);
            GameManager.OnGameStart += UpdateScore;
            GameManager.OnPairFound += UpdateScore;
            GameManager.OnPairFail += UpdateScore;
        }

        private void OnDisable()
        {
            m_ResetButton.onClick.RemoveListener(ResetGame);
            GameManager.OnPairFound -= UpdateScore;
            GameManager.OnPairFail -= UpdateScore;
            GameManager.OnGameStart -= UpdateScore;

        }

        private void ResetGame()
        {
            GameManager.Instance.ResetGame();
        }

        private void UpdateScore()
        {
            m_ScoreText.text = $"Score: {GameManager.Instance.CurrentScore}";
            m_HighScoreText.text = $"High Score: {GameManager.Instance.HighScore}";
            m_ComboText.text = $"Combo: {GameManager.Instance.CurrentCombo}";
        }
    }
}
