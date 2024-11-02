using System;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts.UI
{
    public class CompleteScreen : ScreenBase
    {
        [SerializeField]
        private Button m_ResetButton;

        private void OnEnable()
        {
            m_ResetButton.onClick.AddListener(ResetGame);
        }

        private void OnDisable()
        {
            m_ResetButton.onClick.RemoveListener(ResetGame);
        }

        private void ResetGame()
        {
            GameManager.Instance.ResetGame();
        }
        
    }
}
