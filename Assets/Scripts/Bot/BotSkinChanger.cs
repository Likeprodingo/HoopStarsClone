using System;
using GameController;
using Menu;
using UnityEngine;

namespace Bot
{
    public class BotSkinChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer = default;
        private void OnEnable()
        {
            SkinChangerScript.BotSkinChanged += SkinChangerScript_BotSkinChanged;
        }
        
        private void OnDisable()
        {
            SkinChangerScript.BotSkinChanged -= SkinChangerScript_BotSkinChanged;
        }

        private void SkinChangerScript_BotSkinChanged()
        {
            foreach (var skin in AssetManager.Instance.CircleSkins)
            {
                if (skin._isBot)
                {
                    _renderer.material = skin._material;
                    break;
                }
            }
        }
    }
}