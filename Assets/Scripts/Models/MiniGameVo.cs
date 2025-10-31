using System;

namespace Models
{
    [Serializable]
    public class MiniGameVo
    {
        public string Description;
        public MiniGameItemVo[] MiniGameItemVos;
    }
}