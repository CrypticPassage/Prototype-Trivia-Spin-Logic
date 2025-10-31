using UnityEngine;
using Utils;

namespace Services.Impls
{
    /// <summary>
    /// Даний сервіс предоставляє методи для завантаження конкретної сцени.
    /// </summary>
    public class LoadingService : MonoBehaviour, ILoadingService
    {
        public void LoadScene(string sceneName)
        {
            SceneLoader.SetNextScene(sceneName);
            SceneLoader.LoadSceneByName(SceneNames.LoadingScene);
        }
    }
}