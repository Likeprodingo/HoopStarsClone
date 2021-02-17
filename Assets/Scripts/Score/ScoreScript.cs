using System;
using System.Collections;
using GameController;
using Timer;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    public class ScoreScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _playerText = default;
        [SerializeField] private TextMeshProUGUI _botText = default;
        [SerializeField] private Canvas _winCanvas = default;
        [SerializeField] private Canvas _menuCanvas = default;
        [SerializeField] private float _shineTime = 0.2f;

        private static ScoreScript _instance;
        
        private const int Zero = 0;
        private int _winScoreCount = 3;
        
        public static event Action GameWin = delegate {  };
        public static event Action GameLose = delegate {  };
        
        private int _playerScore = 0;
        private int _botScore = 0;

        private Material _botBaseMaterial;
        private Material _botLightMaterial;
        private Material _playerBaseMaterial;
        private Material _playerLightMaterial;

        public static ScoreScript Instance => _instance;

        public int PlayerScore => _playerScore;

        public int BotScore => _botScore;

        public int WinScoreCount
        {
            get => _winScoreCount;
            set => _winScoreCount = value;
        }

        private void Awake()
        {
            _instance = this;
            _botBaseMaterial = _botText.fontSharedMaterial;
            _playerBaseMaterial = _botText.fontSharedMaterial;

            _botLightMaterial = new Material(_botBaseMaterial);
            _playerLightMaterial = new Material(_playerBaseMaterial);

            _botLightMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1.0f);
            _playerLightMaterial.SetFloat(ShaderUtilities.ID_GlowPower, 1.0f);
        }

        private void OnEnable()
        {
            CollectorScript.EarnedPoint += CollectorScript_EarnedPoint;
            GameManager.GameStarted += GameManager_GameStarted;
            TimerScript.TimeOut += TimerScript_TimeOut;
        }
        
        private void OnDisable()
        {
            CollectorScript.EarnedPoint -= CollectorScript_EarnedPoint;
            GameManager.GameStarted -= GameManager_GameStarted;
            TimerScript.TimeOut -= TimerScript_TimeOut;
        }
        
        private void TimerScript_TimeOut()
        {
            if (_playerScore >= _botScore)
            {
                GameWin?.Invoke();
                _winCanvas.gameObject.SetActive(true);
            }
            else
            {
                _menuCanvas.gameObject.SetActive(true);
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
            _botText.fontSharedMaterial = _botLightMaterial;
            yield return new WaitForSeconds(_shineTime);
            _botText.fontSharedMaterial = _botBaseMaterial;
        }
        
        private IEnumerator PlayerScoreShining()
        {
            _playerText.fontSharedMaterial = _playerLightMaterial;
            yield return new WaitForSeconds(_shineTime);
            _playerText.fontSharedMaterial = _playerBaseMaterial;
        }
    }
}
