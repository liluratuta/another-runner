namespace AnotherRunner.Model.Timers
{
    public interface ITimer
    {
        void Add(float duration, ITimerCommand timerCommand);
        void Repeat(float interval, ITimerCommand timerCommand);
    }
}