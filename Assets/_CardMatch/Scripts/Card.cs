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
        private int m_CardID;
        public int CardID => m_CardID;
        private bool m_IsRevealed;
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
            if (m_IsRevealed)
            {
                return;
            }
            if (m_ClickCallback != null)
            {
                m_ClickCallback?.Invoke(this);
            }
        }
    
        public void Reveal()
        {
            if (GameManager.Instance.IsGameStarted)
            {
                m_IsRevealed = true;

            }
            m_Animator.SetBool(k_Reveal,true);
        }

        public void RevealAnimationComplete()
        {

            if (m_RevealCallback != null)
            {
                m_RevealCallback?.Invoke(this);
            }
        }


        public void Hide(float i_Delay = 0)
        {
            if (i_Delay == 0)
            {
                HideAnimation();
            }
            else
            {
                Invoke(nameof(HideAnimation),i_Delay);
            }

        }

        private void HideAnimation()
        {
            m_Animator.SetBool(k_Reveal,false);

        }
        public void HideAnimationComplete()
        {
            m_IsRevealed = false;


        }
        public void SetListener(Action<Card> i_ClickCallback,Action<Card> i_RevealCallback)
        {
            m_ClickCallback = i_ClickCallback;
            m_RevealCallback = i_RevealCallback;
        }

        public void Initialize(int i_Index, Color i_Color, Sprite i_Graphic)
        {
            m_CardID = i_Index;
            m_BackgroundImage.color = i_Color;
            m_CardGraphic.sprite = i_Graphic;
            m_IsRevealed = false;
            //Debug.Log("m_Animator.isInitialized="+m_Animator.isInitialized);
            m_Animator.SetBool(k_Reveal,false);
            m_Animator.Rebind();
            m_Animator.Update(0f);
            m_Animator.Play("Card_idle_Hidden", -1, 0f);

        }
    }
}
