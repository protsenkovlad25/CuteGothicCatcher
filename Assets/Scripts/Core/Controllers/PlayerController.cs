using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CuteGothicCatcher.Core.Controllers
{
    public class PlayerController : Controller
    {
        private PlayerData m_PlayerData;

        public PlayerData PlayerData => m_PlayerData;

        public override void Init()
        {
            LoadPlayer();
        }

        public void LoadPlayer()
        {

        }
        public void SavePlayer()
        {

        }
    }
}
