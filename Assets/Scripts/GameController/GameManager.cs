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
        [SerializeField] private int _timerCount = 60;
        
        public static event Action GameEnded = delegate { };
        public static event Action GameStarted = delegate { };
        public static event Action TimeOut = delegate {  };
        
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

        private IEnumerator Timer()
        {
            var waiter = new WaitForSeconds(1);
            for (int i = 0; i < _timerCount; i++)
            {
                yield return waiter;
            }
            TimeOut?.Invoke();
        }
    }
}