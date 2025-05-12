using CuteGothicCatcher.Core.Interfaces;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    [System.Serializable]
    public abstract class Quest : MonoBehaviour, IIniting
    {
        public System.Action OnCompleted;
        public System.Action OnChangedProgress;

        [SerializeField] protected QuestData m_Data;

        public QuestData Data => m_Data;

        public virtual void Init()
        {
            Subscribe();
        }

        protected abstract void Subscribe();
        protected abstract void Unsubscribe();

        public virtual void SetaData(QuestData data)
        {
            m_Data = data;
        }

        public virtual void GiveReward()
        {
            if (m_Data.IsComplete && !m_Data.IsReceived)
            {
                m_Data.GiveReward();
            }
        }

        public virtual void UpdateProgress(float value, bool isTotal = false)
        {
            if (!m_Data.IsComplete)
            {
                m_Data.SetProgress(value, isTotal);

                if (m_Data.IsComplete)
                    Complete();

                OnChangedProgress?.Invoke();
            }
            else return;
        }

        public virtual void Complete()
        {
            OnCompleted?.Invoke();
            EventManager.CompleteQuest(m_Data.Id);

            Unsubscribe();
        }

        protected virtual void OnDestroy()
        {
            Unsubscribe();
        }
    }
}
