using Models;
using UnityEngine;

namespace Databases.Impls
{
    [CreateAssetMenu(menuName = "Databases/MiniGameDatabase", fileName = "MiniGameDatabase")] 
    public class MiniGameDatabase : ScriptableObject, IMiniGameDatabase
    {
        [SerializeField] private MiniGameVo[] _miniGameVos;

        public MiniGameVo[] MiniGameVos => _miniGameVos;
    }
}