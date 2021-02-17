using System;
using System.Collections;
using Menu;
using Score;
using UnityEngine;
using Util;

namespace GameController
{
    public class GameManager : GameObjectSingleton<GameManager>
    {

        [SerializeField] private float _slowMotionCount = 0.5f;
        public static event Action GameEnded = delegate { };
        public static event Action GameStarted = delegate { };
        
        protected override void Init()
        {
            base.Init();
            ScoreScript.GameWin += PlayerScore_GameWin;
            ScoreScript.GameLose += PlayerScore_GameLose;
        }
        
        protected override void DeInit()
        {
            ScoreScript.GameWin -= PlayerScore_GameWin;
            ScoreScript.GameLose -= PlayerScore_GameLose;
        }

        private void PlayerScore_GameLose()
        {
            EndLevel();
        }
        
        private void PlayerScore_GameWin()
        {
            EndLevel();
        }
        
        private void EndLevel()
        {
            Time.timeScale = _slowMotionCount;
            GameEnded?.Invoke();
        }

        public void StartLevel()
        {
            Time.timeScale = 1;
            GameStarted?.Invoke();
        }
    }
}