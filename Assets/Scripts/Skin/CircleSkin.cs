using System;
using Score;
using UnityEngine;

namespace Skin
{
    [Serializable]
    public struct CircleSkin
    {
        public Material _material;
        public bool _isPlayer;
        public bool _isBot;
        public bool _isAvailable;
    }
}