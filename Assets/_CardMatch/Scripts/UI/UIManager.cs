using System;
using UnityEngine;

namespace _CardMatch.Scripts.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public CardGrid CardGrid;
        public InGameScreen InGameScreen;
        public StartScreen StartScreen;
        public CompleteScreen CompleteScreen;

        private void OnEnable()
        {
            GameManager.OnGameStart += OnGameStart;
            GameManager.OnGameComplete += OnGameComplete;
            GameManager.OnGameReset += OnGameReset;
        }

        public override void OnDisable()
        {
            base.OnDisable();
            GameManager.OnGameStart -= OnGameStart;
            GameManager.OnGameComplete -= OnGameComplete;
            GameManager.OnGameReset -= OnGameReset;
        }

        private void OnGameStart()
        {
            CardGrid.Show();
            InGameScreen.Show();
            StartScreen.Hide();
            CompleteScreen.Hide();
        }

        private void OnGameComplete()
        {
            CardGrid.Hide();
            InGameScreen.Hide();
            StartScreen.Hide();
            CompleteScreen.Show();
        }

        private void OnGameReset()
        {
            CardGrid.Show();
            InGameScreen.Hide();
            StartScreen.Show();
            CompleteScreen.Hide();
        }
    }
}
