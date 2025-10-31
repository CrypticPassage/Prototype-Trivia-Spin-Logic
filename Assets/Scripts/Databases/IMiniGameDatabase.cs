using Models;

namespace Databases
{
    public interface IMiniGameDatabase
    {
        MiniGameVo[] MiniGameVos { get; }
    }
}