using UnityEngine;
using Utils;

namespace Services.Impls
{
    public class LoadingService : MonoBehaviour, ILoadingService
    {
        public void LoadScene(string sceneName)
        {
            SceneLoader.SetNextScene(sceneName);
            SceneLoader.LoadSceneByName(SceneNames.LoadingScene);
        }
    }
}