using CuteGothicCatcher.Core.Interfaces;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.UI
{
    public class Background : MonoBehaviour, IIniting
    {
        [Header("Positions")]
        [SerializeField] private Vector2 m_RightPos;
        [SerializeField] private Vector2 m_LeftPos;
        [Header("Anim Times")]
        [SerializeField] private float m_MoveTime;

        private bool m_State;
        private Transform m_Transform;

        public void Init()
        {
            m_Transform = GetComponent<Transform>();

            SetRightPos();
        }

        private void SetRightPos()
        {
            m_State = true;
            m_Transform.localPosition = m_RightPos;
        }

        private void SetLeftPos()
        {
            m_State = false;
            m_Transform.localPosition = m_LeftPos;
        }

        public void Move(UnityAction onEndAction = null)
        {
            m_State = !m_State;

            Vector2 curPos = m_State ? m_RightPos : m_LeftPos;

            Sequence s = DOTween.Sequence();

            s.Append(m_Transform.DOLocalMoveX(curPos.x, m_MoveTime));

            if (onEndAction != null)
                s.AppendCallback(onEndAction.Invoke);

            s.SetUpdate(true);
        }
    }
}
