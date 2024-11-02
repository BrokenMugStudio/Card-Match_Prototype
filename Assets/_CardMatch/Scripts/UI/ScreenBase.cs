using UnityEngine;

namespace _CardMatch.Scripts.UI
{
    public class ScreenBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_Holder;

        public virtual void Show()
        {
            m_Holder.SetActive(true);
        }

        public virtual void Hide()
        {
            m_Holder.SetActive(false);

        }
    }
}
