using System;
using System.Collections;
using Effect;
using UnityEngine;

namespace Score
{
    public class CollectorScript : MonoBehaviour
    {
        [SerializeField] private float _collectDelay = 0.1f;
        [SerializeField] private PlayerType _type = default;
        
        public enum PlayerType
        {
            Player,
            Bot
        }
        
        public static event Action<PlayerType> EarnedPoint = delegate {  };
        
        private bool _collected;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BallScript ball) && !_collected)
            {
                StartCoroutine(CollectDelay());
                EarnedPoint?.Invoke(_type);
            }
        }

        private IEnumerator CollectDelay()
        {
            _collected = true;
            yield return new WaitForSeconds(_collectDelay);
            _collected = false;
        }
    }
}
