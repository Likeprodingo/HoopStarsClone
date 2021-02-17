using System;
using System.Collections;
using Effect;
using GameController;
using UnityEngine;
using UnityEngine.Serialization;

namespace Bot
{
    public class BotJump : MonoBehaviour
    {
        [SerializeField] private float _jumpDelay = 1f;
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private Vector3 _jumpDirection = default;
        [SerializeField] private Vector3 _startPosition = default;
        
        private Transform _ball;
        private Transform _currentTransform;
        private JumpDirection _type;
        
        private static BotJump instance;

        public static BotJump Instance => instance;

        private Coroutine _moving;

        private enum JumpDirection
        {
            RIGHT,
            LEFT,
            DOWN
        }
        
        private void Awake()
        {
            instance = this;
            
            _currentTransform = transform;
        }

        private void OnEnable()
        {
            GameManager.GameStarted += GameManager_GameStarted;
            GameManager.GameEnded += GameManager_GameEnded;
        }
        
        private void OnDisable()
        {
            GameManager.GameEnded -= GameManager_GameEnded;
            GameManager.GameStarted -= GameManager_GameStarted;
        }

        private void GameManager_GameEnded()
        {
            StopCoroutine(_moving);
            _moving = null;
        }
        
        private void GameManager_GameStarted()
        {
            if (ReferenceEquals(_moving, null))
            {
                _moving = StartCoroutine(Move());
            }
        }

        public void ResetPosition()
        {
            _currentTransform.position = _startPosition;
            _type = JumpDirection.DOWN;
        }

        private IEnumerator Move()
        {
            var waiter = new WaitForSeconds(_jumpDelay);
            while (true)
            {
                Jump(CalculateDirection());
                yield return waiter;
            }
        }

        private void Jump(Vector3 jump)
        {
            if (_type != JumpDirection.DOWN)
            {
                _rigidbody.velocity = Vector3.zero;
            }
            _rigidbody.AddForce(jump,ForceMode.Impulse);
        }

        private Vector3 CalculateDirection()
        {
            var dir = _jumpDirection;
            if (_ball.position.y < _currentTransform.position.y)
            {
                if (_type == JumpDirection.DOWN)
                {
                    return Vector3.zero;
                }
                else
                {
                    _type = JumpDirection.DOWN;
                    dir.x *= _ball.position.x < _currentTransform.position.x ? -1 : 1;
                }
            }
            else
            {
                switch (_type)
                {
                    case JumpDirection.LEFT:
                        _type = JumpDirection.RIGHT;
                        break;
                    case JumpDirection.RIGHT:
                        _type = JumpDirection.LEFT;
                        dir.x *= -1;
                        break;
                    default:
                        if (_ball.position.x < _currentTransform.position.x)
                        {
                            _type = JumpDirection.RIGHT;
                        }
                        else
                        {
                            _type = JumpDirection.LEFT;
                            dir.x *= -1;
                        }
                        break;
                        
                }
            }
            return dir;
        }

        public void SetAim(BallScript ball)
        {
            _ball = ball.transform;
        }
    }
}