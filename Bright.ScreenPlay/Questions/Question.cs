namespace Bright.ScreenPlay.Questions
{
    public interface IQuestion<TAnswer> : IPerformable<TAnswer>
    {
    }
    public abstract class Question<TAnswer> : Performable<TAnswer>, IQuestion<TAnswer>
    { 
        
    }
}
