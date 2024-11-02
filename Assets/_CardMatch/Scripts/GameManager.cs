using UnityEngine;

namespace _CardMatch.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GameEvent();
        public static GameEvent OnGameStart = delegate { };
        public static GameEvent OnGameComplete = delegate { };
        public static GameEvent OnGameReset = delegate { };
        
        public int DifficultyLevel;
        public bool IsGameStarted;

        public override void Start()
        {
            base.Start();
            ResetGame();
        }

        public void StartGame()
        {
            IsGameStarted = false;
            if (OnGameStart != null)
            {
                OnGameStart?.Invoke();
            }
        }
        public void GameComplete()
        {
            if (OnGameComplete != null)
            {
                OnGameComplete?.Invoke();
            }
        }
        public void ResetGame()
        {
            if (OnGameReset != null)
            {
                OnGameReset?.Invoke();
            }
        }
    }
}
