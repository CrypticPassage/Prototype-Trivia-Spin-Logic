using System.Threading.Tasks;
using Firebase.Database;
using UnityEngine;

namespace Utils
{
    public static class FirebaseDatabaseUtils
    {
        public static async Task<object> GetValueFromDatabaseAsync(string key)
        {
            try
            {
                var snapshot = await FirebaseDatabase.DefaultInstance.GetReference(key).GetValueAsync();

                if (snapshot.Exists) 
                    return snapshot.Value;
                
                Debug.LogWarning($"⚠The '{key}' is not found in the database.");
                
                return null;

            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error while reading key {key}: {e}");
                
                return null;
            }
        }
    }
}