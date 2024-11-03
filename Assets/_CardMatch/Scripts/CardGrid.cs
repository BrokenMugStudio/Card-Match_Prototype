using System;
using System.Collections.Generic;
using _CardMatch.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _CardMatch.Scripts
{
    public class CardGrid : ScreenBase
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
            GameManager.OnGameStart += InitializeLevel;
        }

        private void OnDisable()
        {
            GameManager.OnGameStart -= InitializeLevel;

        }

        [SerializeField]
        private RectTransform m_CanvasRect;
        private Vector2 ScreenResolution
        {
            get
            {
                return new Vector2(m_CanvasRect.rect.width ,m_CanvasRect.rect.height );
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
                    newCard.transform.SetParent(m_CardHolderRect);
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
            if (GameManager.Instance.IsGameStarted)
            {
                GameManager.Instance.CardClicked();

                i_Card.Reveal();

            }
        }

        private void CardRevealed(Card i_Card)
        {
            
            if (!GameManager.Instance.IsGameStarted)
            {
               return;
            }
            m_CardsQueue.Add(i_Card);
            if (m_CardsQueue.Count % 2 == 0)
            {
                var cardA=m_CardsQueue[^2];
                var cardB=i_Card;
                if (cardA.CardID == i_Card.CardID)
                {
                    m_PairsFound.Add(cardA);
                    m_PairsFound.Add(cardB);
                    GameManager.Instance.PairFound();
                    CheckIfCompleted();
                    
                }
                else
                {
                    cardA.Hide(GameConfig.Instance.CardHideDelay);
                    cardB.Hide(GameConfig.Instance.CardHideDelay);
                    GameManager.Instance.PairFail();

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
            var isComplete =(m_PairsFound.Count==m_CardsPool.Count-(m_CardsPool.Count%2));
            if (isComplete)
            {
                GameManager.Instance.GameComplete();
            }
        }
        
        private void InitializeLevel()
        {
            m_GridSize = GameConfig.Instance.GetGridSize(GameManager.Instance.DifficultyLevel);
            InitializeCards();
            AdaptLayout();
            RevealAllCards(GameConfig.Instance.InitialRevealTime);
        }

        private void RevealAllCards(float i_Duration)
        {
            for (int i = 0; i < m_UsedCards.Length; i++)
            {
                m_UsedCards[i].Reveal();
            }
            Invoke(nameof(HideAllCards),i_Duration);
        }

        private void HideAllCards()
        {
            for (int i = 0; i < m_UsedCards.Length; i++)
            {
                m_UsedCards[i].Hide();
            }

            GameManager.Instance.IsGameStarted = true;
        }
    }
}
