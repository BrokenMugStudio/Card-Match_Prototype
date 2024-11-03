using BrokenMugStudioSDK;
using UnityEngine;
using UnityEngine.Serialization;

namespace _CardMatch.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public Gradient CardColors;
        public Sprite[] CardGraphics;
        [FormerlySerializedAs("InitialShowTime")] public float InitialRevealTime = 4;
        public float CardHideDelay = 1;
        public Vector2Int[] GridSizeByDifficulty;

        public Vector2Int GetGridSize(int i_Difficulty)
        {
            return GridSizeByDifficulty[Mathf.Min(i_Difficulty,GridSizeByDifficulty.Length-1)];
        }
    }
}
