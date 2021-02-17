using System;
using GameController;
using Menu;
using UnityEngine;

namespace Control
{
    public class PlayerSkinChanger : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _renderer = default;
        private void OnEnable()
        {
            SkinChangerScript.PlayerSkinChanged += SkinChangerScript_PlayerSkinChanged;
        }

        private void OnDisable()
        {
            SkinChangerScript.PlayerSkinChanged -= SkinChangerScript_PlayerSkinChanged;
        }

        private void SkinChangerScript_PlayerSkinChanged()
        {
            foreach (var skin in AssetManager.Instance.CircleSkins)
            {
                if (skin._isPlayer)
                {
                    _renderer.material = skin._material;
                    break;
                }
            }
        }
    }
}