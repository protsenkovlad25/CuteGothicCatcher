using CuteGothicCatcher.Core;
using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Entities.Components
{
    public class FlyMovement : MonoBehaviour, IMoving
    {
        [SerializeField] private float m_Force;

        private bool m_IsStop;
        private bool m_IsMoving;

        private Rigidbody2D m_Rigidbody;
        private Transform m_Transform;
        private Vector2 m_LastVelocity = Vector2.zero;

        public void Init(Rigidbody2D rigidbody, Transform transform)
        {
            m_Rigidbody = rigidbody;
            m_Transform = transform;
        }

        public void Move()
        {
        }

        private void FirstMove()
        {
            Vector2 spawnPos,
                    targetPoint,
                    direction;

            Vector2 screenMin = MainCamera.ViewportMin,
                    screenMax = MainCamera.ViewportMax;

            spawnPos = transform.position;
            targetPoint = new Vector2(Random.Range(screenMin.x + 5f, screenMax.x - 5f),
                                      Random.Range(screenMin.y + 5f, screenMax.y - 5f));
            
            direction = (targetPoint - spawnPos).normalized;
            
            m_Rigidbody.AddForce(direction * m_Force);
        }

        public void StartMove()
        {
            m_IsStop = false;

            if (!m_IsMoving)
            {
                m_IsMoving = true;

                if (m_LastVelocity == Vector2.zero)
                {
                    FirstMove();
                }
                else
                {
                    m_Rigidbody.velocity = m_LastVelocity;
                }
            }
        }

        public void StopMove()
        {
            m_IsStop = true;
            m_IsMoving = false;

            m_LastVelocity = m_Rigidbody.velocity;
            m_Rigidbody.velocity = Vector2.zero;
        }

        public void DisableMove()
        {
            m_LastVelocity = Vector2.zero;
            m_IsMoving = false;
        }
    }
}
