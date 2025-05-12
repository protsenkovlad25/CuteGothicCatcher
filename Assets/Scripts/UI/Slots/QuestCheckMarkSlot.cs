using CuteGothicCatcher.Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CuteGothicCatcher.UI
{
    public class QuestCheckMarkSlot : MonoBehaviour, IIniting
    {
        [SerializeField] private Image m_CheckMark; 

        public void Init()
        {
            m_CheckMark.gameObject.SetActive(false);
            m_CheckMark.transform.localScale = Vector3.zero;
        }

        public void Complete()
        {
            m_CheckMark.gameObject.SetActive(true);

            Sequence s = DOTween.Sequence();

            s.Append(m_CheckMark.transform.DOScale(2f, .2f));
            s.Append(m_CheckMark.transform.DOScale(1, .2f));
        }
    }
}
