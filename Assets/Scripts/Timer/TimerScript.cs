using System;
using System.Collections;
using System.Collections.Generic;
using GameController;
using TMPro;
using UnityEngine;

namespace Timer
{
    public class TimerScript : MonoBehaviour
    {
        [SerializeField] private int _criticalTime = 10;
        [SerializeField] private int _timerCount = 60;
        [SerializeField] private TextMeshProUGUI _timerText;

        public static event Action TimeOut = delegate {  };
        
        private Coroutine _timer;
        
        private void OnEnable()
        {
            GameManager.GameStarted += GameManager_GameStarted;
            GameManager.GameEnded += GameManager_GameEnded;
        }
        
        private void OnDisable()
        {
            GameManager.GameStarted -= GameManager_GameStarted;
            GameManager.GameEnded -= GameManager_GameEnded;
        }

        private void GameManager_GameEnded()
        {
            StopCoroutine(_timer);
        }
        
        private void GameManager_GameStarted()
        {
            _timer = StartCoroutine(Timer());
        }
        
        private IEnumerator Timer()
        {
            var waiter = new WaitForSeconds(1);
            
            for (int i = _timerCount; i > _criticalTime; i--)
            {
                _timerText.text = i.ToString();
                yield return waiter;
            }
            
            _timerText.color = Color.red;
            
            for (int i = _criticalTime; i >= 0; i--)
            {
                _timerText.text = i.ToString();
                yield return waiter;
            }
            TimeOut?.Invoke();
        }
    }
}