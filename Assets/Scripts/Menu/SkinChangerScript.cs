using System;
using GameController;
using Skin;
using UnityEngine;

namespace Menu
{
    public class SkinChangerScript : MonoBehaviour
    {
        
        public static event Action BotSkinChanged = delegate {  };
        public static event Action PlayerSkinChanged = delegate {  };
        
        public void ChangeBallSkin()
        {
            BallSkin[] ballSkins = AssetManager.Instance.BallSkins;
            for (int i = 0; i < ballSkins.Length; i++)
            {
                if (ballSkins[i]._isActive)
                {
                    ballSkins[i]._isActive = false;
                    if (i < ballSkins.Length - 1)
                    {
                        ballSkins[i + 1]._isActive = true;
                    }
                    else
                    {
                        ballSkins[0]._isActive = true;
                    }
                    break;
                }
            }
        }

        public void ChangePlayerSkin()
        {
            CircleSkin[] circleSkins = AssetManager.Instance.CircleSkins;
            for (int i = 0; i < circleSkins.Length; i++)
            {
                if (circleSkins[i]._isPlayer)
                {
                    circleSkins[i]._isPlayer = false;
                    if (i < circleSkins.Length - 1)
                    {
                        circleSkins[i + 1]._isPlayer = true;
                    }
                    else
                    {
                        circleSkins[0]._isPlayer = true;
                    }
                    PlayerSkinChanged?.Invoke();
                    break;
                }
            }
        }

        public void ChangeBotSkin()
        {
            CircleSkin[] circleSkins = AssetManager.Instance.CircleSkins;
            for (int i = 0; i < circleSkins.Length; i++)
            {
                if (circleSkins[i]._isBot)
                {
                    circleSkins[i]._isBot = false;
                    if (i < circleSkins.Length - 1)
                    {
                        circleSkins[i + 1]._isBot = true;
                    }
                    else
                    {
                        circleSkins[0]._isBot = true;
                    }
                    BotSkinChanged?.Invoke();
                    break;
                }
            }
        }
    }
}