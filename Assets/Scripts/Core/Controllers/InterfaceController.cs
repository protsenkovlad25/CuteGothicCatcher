using CuteGothicCatcher.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CuteGothicCatcher.Core.Controllers
{
    public class InterfaceController : Controller
    {
        [SerializeField] private List<Panel> m_Panels;
        [SerializeField] private BlackScreen m_BlackScreen;
        [SerializeField] private Background m_Background;

        public BlackScreen BlackScreen => m_BlackScreen;

        public override void Init()
        {
            foreach (var panel in m_Panels)
                panel.Init();

            m_BlackScreen.Init();
            m_Background.Init();
        }

        public void OpenPanel(Type type, UnityAction onEndAction = null)
        {
            if (m_Panels.Exists(p => p.GetType() == type))
                OpenPanel(m_Panels.Find(p => p.GetType() == type), onEndAction);
            else
                throw new NotImplementedException($"Not found panel with type \"{type}\"");
        }
        public void OpenPanel(Panel panel, UnityAction onEndAction)
        {
            panel.Open(onEndAction);
        }
        public void OpenPanel(Panel panel)
        {
            panel.Open();
        }

        public void ClosePanel(Type type, UnityAction onEndAction = null)
        {
            if (m_Panels.Exists(p => p.GetType() == type))
                ClosePanel(m_Panels.Find(p => p.GetType() == type), onEndAction);
            else
                throw new NotImplementedException($"Not found panel with type \"{type}\"");
        }
        public void ClosePanel(Panel panel, UnityAction onEndAction)
        {
            panel.Close(onEndAction);
        }
        public void ClosePanel(Panel panel)
        {
            panel.Close();
        }
        public void CloseAllOpenedPanels()
        {
            foreach (var panel in m_Panels)
            {
                if (panel.gameObject.activeSelf)
                {
                    ClosePanel(panel);
                }
            }
        }

        public void MoveBackground(UnityAction onEndAction = null)
        {
            m_Background.Move(onEndAction);
        }
    }
}
