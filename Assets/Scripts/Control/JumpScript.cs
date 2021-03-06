﻿using System;
using UnityEngine;
using DG.Tweening;

namespace Control
{
    public class JumpScript : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody = default;
        [SerializeField] private Vector3 _jumpDirection = default;
        [SerializeField] private Vector3 _startPosition = default;
        
        private static JumpScript _instance;
        private Transform _currentTransform;

        public static JumpScript Instance => _instance;
        private void Awake()
        {
            _currentTransform = transform;
            _instance = this;
        }

        private void OnEnable()
        {
            TapController.Jumped += TapController_Jumped;
        }

        private void OnDisable()
        {
            TapController.Jumped -= TapController_Jumped;
        }
        
        private void TapController_Jumped(TapController.Direction direction)
        {
            _rigidbody.velocity = Vector3.zero;
            Vector3 jump = _jumpDirection;
            jump.x *= direction == TapController.Direction.LEFT ? -1 : 1;
            _rigidbody.AddForce(jump,ForceMode.Impulse);
        }
        
        public void ResetPosition()
        {
            _currentTransform.position = _startPosition;
        }
        
        public void CenterPosition()
        {
            _currentTransform.position = _startPosition;
        }
    }
}