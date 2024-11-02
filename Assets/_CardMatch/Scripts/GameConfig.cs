using BrokenMugStudioSDK;
using UnityEngine;

namespace _CardMatch.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public Gradient CardColors;
        public Sprite[] CardGraphics;
        public float InitialShowTime = 4;
        public float CardHideDelay = 1;
        public Vector2Int[] GridSizeByDifficulty;

        public Vector2Int GetGridSize(int i_Difficulty)
        {
            return GridSizeByDifficulty[Mathf.Min(i_Difficulty,GridSizeByDifficulty.Length-1)];
        }
    }
}
