using System.Collections.Generic;
using GameController;
using UnityEngine;
using Util;

namespace Script.GameController
{
    public class LevelController : GameObjectSingleton<LevelController>
    {
        protected override void Init()
        {
            base.Init();
            GameManager.GameStarted += GameManager_GameStarted;
            GameManager.GameEnded += GameManager_GameEnded;
        }

        protected override void DeInit()
        {
            base.DeInit();
            GameManager.GameStarted -= GameManager_GameStarted;
            GameManager.GameEnded -= GameManager_GameEnded;
        }
        
        private void GameManager_GameEnded()
        {
            
        }

        private void GameManager_GameStarted()
        {
            GenerateLevel();
        }
        
        private void GenerateLevel()
        {
           
        }
    }
}