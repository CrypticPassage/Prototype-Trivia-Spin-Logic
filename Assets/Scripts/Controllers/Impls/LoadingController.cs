using System.Collections;
using UnityEngine;
using Utils;
using Views;

namespace Controllers.Impls
{
    public class LoadingController : MonoBehaviour, ILoadingController
    {
        [SerializeField] private LoadingView _loadingView;
        
        private float _loadingDuration = 2f;
        
        private void Start()
        {
            StartCoroutine(LoadNextScene());
        }

        private IEnumerator LoadNextScene()
        {
            if (SceneLoader.NextSceneName == SceneNames.MenuScene) 
                yield return GetValueFromFirebaseDatabase(FirebaseDatabaseKeys.AnswersCount);
            
            var time = 0f;
            
            while (time < _loadingDuration)
            {
                time += Time.deltaTime;
                _loadingView.ProgressBar.value = Mathf.Clamp01(time / _loadingDuration);
                
                yield return null;
            }
            
            SceneLoader.LoadSceneByName(SceneLoader.NextSceneName);
        }
        
        private static IEnumerator GetValueFromFirebaseDatabase(string key)
        {
            var task = FirebaseDatabaseUtils.GetValueFromDatabaseAsync(key);
            
            yield return new WaitUntil(() => task.IsCompleted);
            
            if (task.Exception != null)
            {
                Debug.LogError($"Error while getting data: {task.Exception}");
                
                yield break;
            }

            if (task.Result == null)
            {
                Debug.LogWarning("Value is not found in the database.");
                
                yield break;
            }

            Debug.Log($"Got value from database: {task.Result} with type: {task.Result.GetType().Name}");
        }
    }
}