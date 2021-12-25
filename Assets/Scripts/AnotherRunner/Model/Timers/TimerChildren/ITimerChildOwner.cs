namespace AnotherRunner.Model.Timers.TimerChildren
{
    public interface ITimerChildOwner
    {
        void Remove(ITimerChild timerChild);
    }
}