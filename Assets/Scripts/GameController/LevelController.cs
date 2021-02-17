using System;
using Bot;
using Control;
using Effect;
using Pool;
using Score;
using UnityEngine;
using Util;

namespace GameController
{
    public class LevelController : GameObjectSingleton<LevelController>
    {
        [SerializeField] private Vector3 _ballStartPosition = default;
        [SerializeField] private string[] _roundNames = new string[4];
        [SerializeField] private int _baseWinCount = 3;
        [SerializeField] private int _finalWinCount = 5;

        public static event Action<string> UpdateLevelName = delegate(string s) {  };
        
        private const int StartRound = 0;
        
        private int _currentRound = StartRound;
        
        protected override void Init()
        {
            base.Init();
            GameManager.GameStarted += GameManager_GameStarted;
            ScoreScript.GameLose += ScoreScript_GameLose;
            ScoreScript.GameWin += ScoreScript_GameWin;
        }
        
        protected override void DeInit()
        {
            base.DeInit();
            GameManager.GameStarted -= GameManager_GameStarted;
            ScoreScript.GameLose -= ScoreScript_GameLose;
            ScoreScript.GameWin -= ScoreScript_GameWin;
        }
        
        private void ScoreScript_GameWin()
        {
            _currentRound++;
            UpdateLevelName?.Invoke(_roundNames[_currentRound]);
        }
        
        private void ScoreScript_GameLose()
        {
            _currentRound = StartRound;
            UpdateLevelName?.Invoke(_roundNames[_currentRound]);
        }

        private void GameManager_GameStarted()
        {
            GenerateLevel();
        }
        
        private void GenerateLevel()
        {
            if (_currentRound > _roundNames.Length)
            {
                _currentRound = 0;
            }
            
            switch (_currentRound)
            {
                case 4:
                    GenerateUsualLevel();
                    ScoreScript.Instance.WinScoreCount = _finalWinCount;
                    break;
                default:
                    ScoreScript.Instance.WinScoreCount = _baseWinCount;
                    GenerateUsualLevel();
                    break;
            }
        }

        private void GenerateUsualLevel()
        {
            BallScript ballScript = ObjectPool.Instance.Get<BallScript>(AssetManager.Instance.Ball, _ballStartPosition);
            JumpScript.Instance.ResetPosition();
            BotJump.Instance.ResetPosition();
            BotJump.Instance.SetAim(ballScript);
        }

        private void GenerateSuperLevel()
        {
            
        } 
    }
}