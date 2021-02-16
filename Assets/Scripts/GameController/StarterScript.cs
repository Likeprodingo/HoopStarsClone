using System.Collections;
using Pool;
using Script.GameController;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameController
{
    public class StarterScript : MonoBehaviour
    {
        private const int FirstScene = 1;
        IEnumerator Start()
        {
            DontDestroyOnLoad(gameObject);
            Init();
            yield return StartCoroutine(LoadScene());
            Destroy(gameObject);
        }

        private void Init()
        {
            GameManager.GetInstance();
            ObjectPool.GetInstance();
            LevelController.GetInstance();
        }

        IEnumerator LoadScene()
        {
            yield return SceneManager.LoadSceneAsync(FirstScene);
        }
    }
}