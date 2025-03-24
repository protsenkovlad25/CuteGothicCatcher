using CuteGothicCatcher.Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class Panel : MonoBehaviour, IIniting
    {
        protected enum Direction { Center, Up, Down, Left, Right }

        [Header("Anim Values")]
        [SerializeField] protected Direction m_Direction;
        [SerializeField] protected float m_OpenTime;
        [SerializeField] protected float m_CloseTime;

        protected Vector2 m_OpenPos;
        protected Vector2 m_ClosePos;

        protected Sequence m_OpenSeq;
        protected Sequence m_CloseSeq;
        protected RectTransform m_RectTransform;

        public virtual void Init()
        {
            m_RectTransform = GetComponent<RectTransform>();
            
            InitStartPosition();
        }

        protected virtual void InitStartPosition()
        {
            if (m_Direction != Direction.Center)
            {
                m_OpenPos = m_RectTransform.anchoredPosition;

                float posX = m_Direction switch
                {
                    Direction.Up => m_RectTransform.anchoredPosition.x,
                    Direction.Down => m_RectTransform.anchoredPosition.x,
                    Direction.Left => -m_RectTransform.sizeDelta.x,
                    Direction.Right => m_RectTransform.sizeDelta.x
                };
                float posY = m_Direction switch
                {
                    Direction.Up => m_RectTransform.sizeDelta.y,
                    Direction.Down => -m_RectTransform.sizeDelta.y,
                    Direction.Left => m_RectTransform.anchoredPosition.y,
                    Direction.Right => m_RectTransform.anchoredPosition.y
                };

                m_ClosePos = new Vector2(posX, posY);
                m_RectTransform.anchoredPosition = m_ClosePos;
            }
        }

        public virtual void Open(UnityAction onEndAction = null)
        {
            OpenAnim(onEndAction);
        }
        public virtual void Close(UnityAction onEndAction = null)
        {
            CloseAnim(onEndAction);
        }

        #region Animations
        protected virtual void OpenAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(true);

            onEndAction?.Invoke();
        }
        protected virtual void CloseAnim(UnityAction onEndAction = null)
        {
            gameObject.SetActive(false);

            onEndAction?.Invoke();
        }
        #endregion
    }
}
