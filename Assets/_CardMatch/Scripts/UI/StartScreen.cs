using System;
using TMPro;
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
        private int m_CurrentDifficulty=5;
        [SerializeField]
        private TextMeshProUGUI m_SizeText;
        
        private void OnEnable()
        {
            m_StartButton.onClick.AddListener(StartGame);
            m_DifficultySlider.onValueChanged.AddListener(DifficultyChanged);
            
            DifficultyChanged(m_CurrentDifficulty);
            m_DifficultySlider.SetValueWithoutNotify(m_CurrentDifficulty);
        }

        private void OnDisable()
        {
            m_StartButton.onClick.RemoveListener(StartGame);
            m_DifficultySlider.onValueChanged.RemoveListener(DifficultyChanged);
        }

        private void DifficultyChanged(float i_Value)
        {
            m_CurrentDifficulty = Mathf.RoundToInt(i_Value);
            var gridSize = GameConfig.Instance.GetGridSize(m_CurrentDifficulty);
            
            m_SizeText.text = "Size : ("+gridSize.x+"x"+gridSize.y+")";

        }

        private void StartGame()
        {
            GameManager.Instance.DifficultyLevel = m_CurrentDifficulty;
            GameManager.Instance.StartGame();
        }
    }
}
