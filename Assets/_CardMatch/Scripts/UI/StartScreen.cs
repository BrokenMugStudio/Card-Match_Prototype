using System;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts.UI
{
    public class StartScreen : ScreenBase
    {
        [SerializeField]
        private Button m_StartButton;
        [SerializeField]
        private Slider m_DifficultySlider;
        private float m_CurrentDifficulty=5;
        
        private void OnEnable()
        {
            m_StartButton.onClick.AddListener(StartGame);
            m_DifficultySlider.onValueChanged.AddListener(DifficultyChanged);
            m_DifficultySlider.SetValueWithoutNotify(m_CurrentDifficulty);
        }

        private void OnDisable()
        {
            m_StartButton.onClick.RemoveListener(StartGame);
            m_DifficultySlider.onValueChanged.RemoveListener(DifficultyChanged);
        }

        private void DifficultyChanged(float i_Value)
        {
            m_CurrentDifficulty = i_Value;
            //GameManager.Instance.DifficultyLevel = Mathf.RoundToInt(m_CurrentDifficulty);

        }

        private void StartGame()
        {
            GameManager.Instance.DifficultyLevel = Mathf.RoundToInt(m_CurrentDifficulty);
            GameManager.Instance.StartGame();
        }
    }
}
