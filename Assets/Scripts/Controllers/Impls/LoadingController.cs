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
            if (SceneLoader.NextSceneName == SceneNames.MinigameScene) 
                yield return GetIntValueFromFirebaseDatabase(FirebaseDatabaseKeys.AnswersCount);
            
            var time = 0f;
            
            while (time < _loadingDuration)
            {
                time += Time.deltaTime;
                _loadingView.ProgressBar.value = Mathf.Clamp01(time / _loadingDuration);
                
                yield return null;
            }
            
            SceneLoader.LoadSceneByName(SceneLoader.NextSceneName);
        }
        
        private static IEnumerator GetIntValueFromFirebaseDatabase(string key)
        {
            var task = FirebaseDatabaseUtils.GetValueFromDatabase(key);
            
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

            int value = 0;
            
            try
            {
                switch (task.Result)
                {
                    case int intVal:
                        value = intVal;
                        break;
                    case long longVal:
                        value = (int)longVal;
                        break;
                    case double doubleVal:
                        value = Mathf.RoundToInt((float)doubleVal);
                        break;
                    case string strVal when int.TryParse(strVal, out var parsed):
                        value = parsed;
                        break;
                    default:
                        Debug.LogWarning($"Unexpected type from Firebase: {task.Result.GetType().Name}");
                        yield break;
                }

                PlayerPrefs.SetInt(PlayerPrefsKeys.AnswersCount, value);
                PlayerPrefs.Save();

                Debug.Log($"Got value from database: {value} (original type: {task.Result.GetType().Name})");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error converting Firebase result to int: {e.Message}");
            }
        }
    }
}