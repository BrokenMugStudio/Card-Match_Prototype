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
        private List<Card> m_CardsQueue;
        [SerializeField]
        private List<Card> m_PairsFound;
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
        [SerializeField]
        private Vector2 m_SizeScale = Vector2.one*.8f;
        private void OnEnable()
        {
            IntializeLevel();
        }

        private void OnDisable()
        {
        }

        private void Update()
        {
            
            
        }
        [SerializeField]
        private Vector2 m_TotalSpacing,m_ScreenMax,m_CardSize;
        [SerializeField]
        private RectTransform m_CanvasRect;
        private Vector2 ScreenResolution
        {
            get
            {
                #if UNITY_EDITOR
                return new Vector2(m_CanvasRect.rect.width ,
                    m_CanvasRect.rect.height );
                #endif
                return new Vector2(Screen.width, Screen.height);
            }
        }
        private void AdaptLayout()
        {
            
            var totalSpacingX=(m_GridSize.x+1)*m_Spacing;
            var totalSpacingY=(m_GridSize.y+1)*m_Spacing;
            var screenMaxWidth=ScreenResolution.x*m_SizeScale.x;
            var screenMaxHeight=ScreenResolution.y*m_SizeScale.y;
            var cardSize = Mathf.Min((float)(screenMaxWidth-totalSpacingX) / m_GridSize.x, (float)(screenMaxHeight-totalSpacingY) / m_GridSize.y);
            var rectSizeX = (cardSize*m_GridSize.x)+totalSpacingX;
            var rectSizeY = (cardSize*m_GridSize.y)+totalSpacingY;
            m_CardHolderRect.sizeDelta = new Vector2(rectSizeX,rectSizeY);
            
            m_GridLayout.cellSize = Vector2.one*cardSize;
            m_GridLayout.spacing = m_Spacing * Vector2.one;
            m_TotalSpacing = new Vector2(totalSpacingX,totalSpacingY);
            m_ScreenMax=new Vector2(screenMaxWidth,screenMaxHeight);
            m_CardSize=new Vector2(cardSize,cardSize);

        }

        private void InitializeCards()
        {
            m_CardsQueue = new List<Card>();
            m_PairsFound = new List<Card>();
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

                    m_UsedCards[i].SetListener(CardClicked,CardRevealed);
                    
                    var index = i / 2;
                    var color=GameConfig.Instance.CardColors.Evaluate((float)index/requiredCardCount);
                    var graphic = GameConfig.Instance.CardGraphics[index % GameConfig.Instance.CardGraphics.Length];
                    m_UsedCards[i].Initialize(index, color, graphic);
                }
                else
                {
                    m_CardsPool[i].SetListener(null,null);

                    m_CardsPool[i].gameObject.SetActive(false);

                }
                
            }

            ShuffleCards();

        }

        private void CardClicked(Card i_Card)
        {
            i_Card.Reveal();
        }

        private void CardRevealed(Card i_Card)
        {
            Debug.Log("CardRevealed");

            m_CardsQueue.Add(i_Card);
            if (m_CardsQueue.Count % 2 == 0)
            {
                Debug.Log("Pair found");

                var cardA=m_CardsQueue[^2];
                var cardB=i_Card;
                if (cardA.CardID == i_Card.CardID)
                {
                    m_PairsFound.Add(cardA);
                    m_PairsFound.Add(cardB);
                    CheckIfCompleted();
                }
                else
                {
                    cardA.Hide();
                    cardB.Hide();
                }

                m_CardsQueue.Remove(cardA);
                m_CardsQueue.Remove(cardB);
            }
        }

        private void ShuffleCards()
        {
            for (int i = 0; i < m_UsedCards.Length; i++)
            {
                m_UsedCards[i].transform.SetSiblingIndex(UnityEngine.Random.Range(0, m_UsedCards.Length));
            }
        }
        private void CheckIfCompleted()
        {
            
        }
        
        private void IntializeLevel()
        {
            InitializeCards();
            AdaptLayout();
            
        }
    }
}
