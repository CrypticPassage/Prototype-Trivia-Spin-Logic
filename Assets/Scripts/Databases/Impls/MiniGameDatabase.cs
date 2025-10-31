using Models;
using UnityEngine;

namespace Databases.Impls
{
    /// <summary>
    /// Дана ДБ містить в собі всі питання та варіанти відповіді на них, що є у грі.
    /// Додаючи чи видаляючи дані, ми регуляємо кількість питань.
    /// </summary>
    [CreateAssetMenu(menuName = "Databases/MiniGameDatabase", fileName = "MiniGameDatabase")] 
    public class MiniGameDatabase : ScriptableObject, IMiniGameDatabase
    {
        [SerializeField] private MiniGameVo[] _miniGameVos;

        public MiniGameVo[] MiniGameVos => _miniGameVos;
    }
}