using CuteGothicCatcher.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CuteGothicCatcher.Core
{
    public class Pool<T> where T : MonoBehaviour, IPoolable
    {
        public System.Action<T> OnCreateNew;
        public System.Action<T> OnReturned;

        private readonly T m_Prefab;
        private readonly int m_InitialCount;
        private readonly Transform m_Parent;
        private readonly Dictionary<T, bool> m_Objects;
        private readonly HashSet<T> m_NewObjects;

        public Dictionary<T, bool> Objects => m_Objects;
        public List<T> ObjectsList
        {
            get
            {
                List<T> list = new List<T>();
                foreach (var enemy in m_Objects)
                    list.Add(enemy.Key);

                return list;
            }
        }
        public HashSet<T> NewObjects => m_NewObjects;

        public Pool(T prefab, int initialCount, Transform parent = null)
        {
            m_Prefab = prefab;
            m_Parent = parent;
            m_InitialCount = initialCount;

            m_Objects = new Dictionary<T, bool>();
            m_NewObjects = new HashSet<T>();

            for (int i = 0; i < initialCount; i++)
            {
                CreateObject();
            }
        }

        private T CreateObject(bool isNew = false)
        {
            T clone = Object.Instantiate(m_Prefab, Vector3.zero, Quaternion.identity);
            clone.OnDisabled += Return;

            clone.transform.SetParent(m_Parent);
            clone.gameObject.SetActive(false);

            if (isNew)
            {
                m_NewObjects.Add(clone);
                OnCreateNew?.Invoke(clone);
            }
            else
            {
                m_Objects.Add(clone, false);
            }

            return clone;
        }
        private void ActivateObject(T obj)
        {
            m_Objects[obj] = true;
            obj.gameObject.SetActive(true);
            obj.transform.SetParent(null);

            obj.OnActivate();
        }
        private void DeactivateObject(T obj)
        {
            obj.OnDeactivate();

            m_Objects[obj] = false;
            obj.gameObject.SetActive(false);

            if (m_Parent)
                obj.transform.SetParent(m_Parent);
        }
        private void DestroyObject(T obj)
        {
            Object.Destroy(obj.gameObject);
        }

        public T Take()
        {
            bool isNeedClearing = false;

            T freeObject = null;
            foreach (var obj in m_Objects)
            {
                if (obj.Key == null)
                {
                    isNeedClearing = true;
                    continue;
                }

                if (obj.Value == true)
                {
                    continue;
                }

                freeObject = obj.Key;
                break;
            }

            if (isNeedClearing)
                Clear();

            if (freeObject != null)
            {
                ActivateObject(freeObject);

                return freeObject;
            }

            T newObject = CreateObject(true);

            ActivateObject(newObject);

            return newObject;
        }
        public void Return(T obj)
        {
            if (m_NewObjects.Contains(obj))
            {
                obj.OnDeactivate();
                m_NewObjects.Remove(obj);
                DestroyObject(obj);
            }
            else
            {
                DeactivateObject(obj);

                OnReturned?.Invoke(obj);
            }
        }
        public void Return(IPoolable poolable)
        {
            Return(poolable as T);
        }
        public void ReturnAll()
        {
            foreach (var obj in m_Objects)
                Return(obj.Key);

            foreach (var obj in m_NewObjects)
                Return(obj);
        }

        private void Clear()
        {
            for (int i = m_Objects.Count - 1; i >= 0; i--)
            {
                if (m_Objects.ElementAt(i).Key == null)
                    m_Objects.Remove(m_Objects.ElementAt(i).Key);
            }
        }
    }
}
