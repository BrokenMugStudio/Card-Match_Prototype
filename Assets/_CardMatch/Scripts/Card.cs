using System;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts
{
    public class Card : MonoBehaviour
    {
        private static readonly int k_Reveal = Animator.StringToHash("Reveal");

        private Action<Card> m_ClickCallback;
        private Action<Card> m_RevealCallback;
        [SerializeField]
        private Image m_BackgroundImage,m_CardGraphic,m_BackImage;
        [SerializeField]
        private Button m_Button;
        [SerializeField]
        private Animator m_Animator;
        
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
            if (m_ClickCallback != null)
            {
                m_ClickCallback?.Invoke(this);
            }
        }
    
        public void Reveal()
        {
            m_Animator.SetBool(k_Reveal,true);
        }

        public void RevealAnimationComplete()
        {
            
        }

       
        public void Hide()
        {
            m_Animator.SetBool(k_Reveal,false);

        }
        public void HideAnimationComplete()
        {
            
        }
        public void SetListener(Action<Card> i_Callback)
        {
            m_ClickCallback = i_Callback;
        }
    }
}
