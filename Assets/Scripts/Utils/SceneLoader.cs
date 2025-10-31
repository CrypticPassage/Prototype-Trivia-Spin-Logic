using UnityEngine.SceneManagement;

namespace Utils
{
    public static class SceneLoader
    {
        private static string _nextSceneName = string.Empty;
        
        public static string NextSceneName => _nextSceneName;
        

        public static void SetNextScene(string sceneName) 
            => _nextSceneName = sceneName;
        
        public static void LoadSceneByName(string sceneName)
        {
            if (sceneName == string.Empty)
            {
                SceneManager.LoadScene(SceneNames.DefaultScene);
                
                return;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}