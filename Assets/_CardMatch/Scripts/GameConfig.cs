using BrokenMugStudioSDK;
using UnityEngine;

namespace _CardMatch.Scripts
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig")]
    public class GameConfig : SingletonScriptableObject<GameConfig>
    {
        public Gradient CardColors;
        public Sprite[] CardGraphics;
    }
}
