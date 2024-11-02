using System;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts
{
    public class Card : MonoBehaviour
    {
        [SerializeField]
        private Image m_BackgroundImage,m_CardGraphic,m_BackImage;
        [SerializeField]
        private Button m_Button;
        
        private void OnEnable()
        {
            m_Button.onClick.AddListener(OnCardClick);
        }

        private void OnDisable()
        {
            m_Button.onClick.RemoveListener(OnCardClick);

        }

        private void OnCardClick()
        {
            
        }
    }
}
