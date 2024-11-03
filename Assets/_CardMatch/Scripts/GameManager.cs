using UnityEngine;

namespace _CardMatch.Scripts
{
    public class GameManager : Singleton<GameManager>
    {
        public delegate void GameEvent();
        public static GameEvent OnGameStart = delegate { };
        public static GameEvent OnGameComplete = delegate { };
        public static GameEvent OnGameReset = delegate { };
        
        
        public static GameEvent OnCardClicked = delegate { };
        public static GameEvent OnPairFound = delegate { };
        public static GameEvent OnPairFail = delegate { };
       
 
        public int DifficultyLevel;
        public bool IsGameStarted;
        public int CurrentScore;
        public int CurrentCombo;

        public int HighScore
        {
            get => PlayerPrefs.GetInt(nameof(HighScore), 0);
            set => PlayerPrefs.SetInt(nameof(HighScore), value);
        }

        public override void Start()
        {
            base.Start();
            ResetGame();
        }

        public void StartGame()
        {
            CurrentScore = 0;
            CurrentCombo = 0;
            
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

        public void PairFound()
        {
            CurrentCombo++;
            CurrentScore+=CurrentCombo;
            HighScore = CurrentScore>HighScore? CurrentScore : HighScore;
            if (OnPairFound != null)
            {
                OnPairFound?.Invoke();
            }
        }

        public void PairFail()
        {
            CurrentCombo = 0;
            if (OnPairFail != null)
            {
                OnPairFail?.Invoke();
            }
        }
        public void CardClicked()
        {
            if (OnCardClicked != null)
            {
                OnCardClicked?.Invoke();
            }
        }
        
    }
}
