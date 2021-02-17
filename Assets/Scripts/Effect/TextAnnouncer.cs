using System;
using System.Collections;
using Bot;
using UnityEngine;
using DG.Tweening;
using Score;

namespace Effect
{
    public class TextAnnouncer : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private RectTransform _transform;
        
        private Sequence _sequence;
        private void OnEnable()
        {
            CollectorScript.EarnedPoint += CollectorScript_EarnedPoint;
        }
        
        private void OnDisable()
        {
            CollectorScript.EarnedPoint -= CollectorScript_EarnedPoint;
        }

        private void CollectorScript_EarnedPoint(CollectorScript.PlayerType obj)
        {
            _sequence = DOTween.Sequence();
            _sequence.Append(_transform.DOScale(Vector3.one, _duration));
            _sequence.Append(_transform.DOScale(Vector3.zero, _duration));
        }
        
    }
}