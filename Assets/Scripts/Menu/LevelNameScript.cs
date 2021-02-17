using System;
using GameController;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class LevelNameScript : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _roundName = default;
        
        private void OnEnable()
        {
            LevelController.UpdateLevelName += LevelController_UpdateLevelName;
        }

        private void LevelController_UpdateLevelName(string roundName)
        {
            _roundName.text = roundName;
        }
    }
}