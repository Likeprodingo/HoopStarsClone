using Effect;
using Skin;
using UnityEngine;
using Util;

namespace GameController
{
    public class AssetManager : GameObjectSingleton<AssetManager>
    {
        [SerializeField] private BallSkin[] _ballSkins = default;
        [SerializeField] private CircleSkin[] _circleSkins = default;
        [SerializeField] private BallScript _ball = default;

        public BallScript Ball => _ball;

        public BallSkin[] BallSkins => _ballSkins;

        public CircleSkin[] CircleSkins => _circleSkins;
    }
}