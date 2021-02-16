using System;
using System.Collections;
using GameController;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    public class ScoreScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerText = default;
        [SerializeField] private TextMeshProUGUI _botText = default;
        [SerializeField] private int _winScoreCount = 3;
        [SerializeField] private Canvas _winCanvas = default;
        [SerializeField] private Canvas _menuCanvas = default;
        [SerializeField] private float _shineTime = 0.2f;
        
        private const int Zero = 0;
        
        public static event Action GameWin = delegate {  };
        public static event Action GameLose = delegate {  };
        
        private int _playerScore = 0;
        private int _botScore = 0;

        public int PlayerScore => _playerScore;

        public int BotScore => _botScore;

        private void OnEnable()
        {
            CollectorScript.EarnedPoint += CollectorScript_EarnedPoint;
            GameManager.GameStarted += GameManager_GameStarted;
            GameManager.TimeOut += GameManager_TimeOut;
        }
        
        private void OnDisable()
        {
            CollectorScript.EarnedPoint -= CollectorScript_EarnedPoint;
            GameManager.GameStarted -= GameManager_GameStarted;
            GameManager.TimeOut -= GameManager_TimeOut;
        }
        
        private void GameManager_TimeOut()
        {
            if (_playerScore >= _botScore)
            {
                GameWin?.Invoke();
            }
            else
            {
                GameLose?.Invoke();
            }
        }

        private void GameManager_GameStarted()
        {
            _playerText.text = Zero.ToString();
            _botText.text = Zero.ToString();
            _playerScore = Zero;
            _botScore = Zero;
        }

        private void CollectorScript_EarnedPoint(CollectorScript.PlayerType type)
        {
            if (type == CollectorScript.PlayerType.Bot)
            {
                StartCoroutine(BotScoreShining());
                _botText.text = (++_botScore).ToString();
                if (_winScoreCount == _botScore)
                {
                    _menuCanvas.gameObject.SetActive(true);
                    GameLose?.Invoke();
                }
            }
            else
            {
                StartCoroutine(PlayerScoreShining());
                _playerText.text = (++_playerScore).ToString();
                if (_winScoreCount == _playerScore)
                {
                    _winCanvas.gameObject.SetActive(true);
                    GameWin?.Invoke();
                }
            }
        }

        private IEnumerator BotScoreShining()
        {
            var startColor = _botText.color;
            _botText.color = Color.Lerp(startColor, Color.yellow, 0.5f );
            yield return new WaitForSeconds(_shineTime);
            _botText.color = startColor;
        }
        
        private IEnumerator PlayerScoreShining()
        {
            var startColor = _playerText.color;
            _playerText.color = Color.Lerp(startColor, Color.yellow, 0.5f );
            yield return new WaitForSeconds(_shineTime);
            _playerText.color = startColor;
        }
    }
}
