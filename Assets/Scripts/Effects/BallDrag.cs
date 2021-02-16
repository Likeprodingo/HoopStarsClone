using UnityEngine;

namespace Effects
{
    public class BallDrag : MonoBehaviour
    {
        private static BallDrag _instance;

        public static BallDrag Instance => _instance;

        private void Awake()
        {
            _instance = this;
        }
    }
}
