using Bright.ScreenPlay.Questions;

namespace Bright.ScreenPlay.Tasks
{
    public interface ITask : IPerformable
    {

    }
    public abstract class Task : Performable, ITask
    {

    }
}
