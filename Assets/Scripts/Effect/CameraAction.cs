using DG.Tweening;
using Score;
using UnityEngine;

namespace Effect
{
    public class CameraAction : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private float _strength = 0.5f;
        [SerializeField] private int _vibrato = 5;
        [SerializeField] private float _randomness = 3f;
        [SerializeField] private bool _fadeOut = true;
        
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

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
            if (obj == CollectorScript.PlayerType.Player)
            {
                _camera.DOShakePosition(_duration,_strength,_vibrato,_randomness,_fadeOut);
            }
        }
    }
}