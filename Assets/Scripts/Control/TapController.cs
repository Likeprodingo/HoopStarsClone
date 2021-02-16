using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Control
{
    public class TapController : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Direction _direction = default;
        
        public enum Direction
        {
            LEFT,
            RIGHT
        }

        public static event Action<Direction> Jumped = delegate { }; 
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Jumped?.Invoke(_direction);
        }
    }
}