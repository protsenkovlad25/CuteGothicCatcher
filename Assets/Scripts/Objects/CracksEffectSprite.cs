using CuteGothicCatcher.Core.Interfaces;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Objects
{
    public class CracksEffectSprite : MonoBehaviour, IPoolable
    {
        private enum CrackVisual { OneByOne, GraduallyEveryone, Combined }

        [SerializeField] private CrackVisual m_Visual;
        [SerializeField] private List<SpriteRenderer> m_CrackSprites;

        public void UpdateCracks(float curValue, float maxValue)
        {
            float percent = 1 - (curValue / maxValue);

            switch (m_Visual)
            {
                case CrackVisual.OneByOne: OneByOne(percent); break;
                case CrackVisual.Combined: Combined(percent); break;
                case CrackVisual.GraduallyEveryone: GraduallyEveryone(percent); break;
            }
        }

        private void OneByOne(float percent)
        {
            float min, max, alpha, step = 1f / m_CrackSprites.Count;
            for (int i = 0; i < m_CrackSprites.Count; i++)
            {
                max = step * (i + 1);
                min = max - step;
                
                m_CrackSprites[i].gameObject.SetActive(percent >= min);
                
                if (percent > min && percent <= max)
                {
                    alpha = (percent - min) / (max - min);
                    m_CrackSprites[i].DOFade(alpha, 0);
                }
            }
        }
        private void GraduallyEveryone(float percent)
        {
            foreach (var crack in m_CrackSprites)
            {
                crack.gameObject.SetActive(true);
                crack.DOFade(percent, 0);
            }
        }
        private void Combined(float percent)
        {
            float step = 1f / m_CrackSprites.Count;
            for (int i = 0; i < m_CrackSprites.Count; i++)
            {
                m_CrackSprites[i].gameObject.SetActive(percent >= step * (i + 1) - step);
            }

            foreach (var crack in m_CrackSprites)
            {
                crack.DOFade(percent, 0);
            }
        }

        public void OnActivate()
        {
        }
        public void OnDeactivate()
        {
            foreach (var crack in m_CrackSprites)
            {
                crack.DOFade(0, 0);
                crack.gameObject.SetActive(false);
            }
        }
    }
}
