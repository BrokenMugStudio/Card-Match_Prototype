using System;
using UnityEngine;

namespace _CardMatch.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource m_AudioSource;
        [SerializeField]
        private AudioClip m_GameCompleteAudioClip, m_CardClickedAudioClip, m_PairFoundAudioClip, m_PairFailedAudioClip;
        
        private void OnEnable()
        {
            GameManager.OnGameComplete += GameComplete;
            GameManager.OnCardClicked += CardClicked;
            GameManager.OnPairFound += PairFound;
            GameManager.OnPairFail += PairFailed;
            
        }

        private void OnDisable()
        {
            GameManager.OnGameComplete -= GameComplete;
            GameManager.OnCardClicked -= CardClicked;
            GameManager.OnPairFound -= PairFound;
            GameManager.OnPairFail -= PairFailed;
        }

        private void PlaySound(AudioClip i_Clip)
        {
            m_AudioSource.PlayOneShot(i_Clip);
        }

        private void GameComplete()
        {
            PlaySound(m_GameCompleteAudioClip);
        }

        private void CardClicked()
        {
            PlaySound(m_CardClickedAudioClip);
        }

        private void PairFound()
        {
            PlaySound(m_PairFoundAudioClip);

        }

        private void PairFailed()
        {
            PlaySound(m_PairFailedAudioClip);

        }
    }
}
