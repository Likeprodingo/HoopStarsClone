using System;
using GameController;
using Pool;
using UnityEngine;
using DG.Tweening;

namespace Effect
{
    public class BallScript : PooledObject
    {
        [SerializeField] private MeshRenderer _meshRenderer = default;
        [SerializeField] private float _duration = 1.0f;
        [SerializeField] private float _strengthDiviser = 3f;
        [SerializeField] private int _vibrato = 3;

        private bool _canBounce = true;

        private Sequence _drag;
        
        public override void SpawnFromPool()
        {
            base.SpawnFromPool();
            _drag = DOTween.Sequence();
            foreach (var skin in AssetManager.Instance.BallSkins)
            {
                if (skin._isActive)
                {
                    _meshRenderer.material = skin._material;
                    break;
                }
            }
        }

       
        private void OnCollisionEnter(Collision collision)
        {
            if (_canBounce)
            {
                _canBounce = false;
                _drag.Append(transform.DOShakePosition(_duration, collision.relativeVelocity.normalized / _strengthDiviser, _vibrato).
                    OnComplete(()=> { _canBounce = true;}));
            }
        }
        
        protected override void BeforeReturnToPool()
        {
            DOTween.Kill(_drag);
        }
    }
}
