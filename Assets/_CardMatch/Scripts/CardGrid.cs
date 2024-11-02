using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts
{
    public class CardGrid : MonoBehaviour
    {
        [SerializeField]
        private List<Card> m_CardsPool;
        [SerializeField]
        private Card[] m_UsedCards;
        [SerializeField]
        private Card m_CardPrefab;
        [SerializeField]
        private Vector2Int m_GridSize=new Vector2Int(3,3);
        [SerializeField]
        private RectTransform m_CardHolderRect;
        [SerializeField]
        private GridLayoutGroup m_GridLayout;
        [SerializeField]
        private float m_Spacing = 16;
        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            IntializeLevel();
            
        }

        private void AdaptLayout()
        {
            
            var totalSpacingX=(m_GridSize.x+1)*m_Spacing;
            var totalSpacingY=(m_GridSize.y+1)*m_Spacing;
            var screenMaxWidth=Screen.width*.9f;
            var screenMaxHeight=Screen.height*.9f;
            var cardSize = Mathf.Min((float)(screenMaxWidth-totalSpacingX) / m_GridSize.x, (float)(screenMaxHeight-totalSpacingY) / m_GridSize.y);
            var rectSizeX = (cardSize*m_GridSize.x)+totalSpacingX;
            var rectSizeY = (cardSize*m_GridSize.y)+totalSpacingY;
            m_CardHolderRect.sizeDelta = new Vector2(rectSizeX,rectSizeY);
            
            m_GridLayout.cellSize = Vector2.one*cardSize;
            m_GridLayout.spacing = m_Spacing * Vector2.one;

        }

        private void CheckCardCount()
        {
            var requiredCardCount = m_GridSize.x*m_GridSize.y;
            if (m_CardsPool.Count < requiredCardCount)
            {
                var cardsToAdd = requiredCardCount - m_CardsPool.Count;
                for (int i = 0; i < cardsToAdd; i++)
                {
                    var newCard = Instantiate(m_CardPrefab, m_CardHolderRect);
                    newCard.transform.parent = m_CardHolderRect;
                    m_CardsPool.Add(newCard);
                }
            }
            m_UsedCards=new Card[requiredCardCount];
            for (int i = 0; i < m_CardsPool.Count; i++)
            {
                if (i < requiredCardCount)
                {
                    m_CardsPool[i].gameObject.SetActive(true);
                    m_UsedCards[i]=m_CardsPool[i];
                }
                else
                {
                    m_CardsPool[i].gameObject.SetActive(false);

                }
                
            }
            
            
        }
        
        private void IntializeLevel()
        {
            CheckCardCount();
            AdaptLayout();
            
        }
    }
}
