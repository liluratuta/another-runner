namespace AnotherRunner.Model.Timers.TimerChildren
{
    public class OneShotTimerChild : ITimerChild
    {
        private readonly ITimerChildOwner _owner;
        private readonly ITimerCommand _timerCommand;
        private float _remainingTime;

        public OneShotTimerChild(ITimerChildOwner owner, float duration, ITimerCommand timerCommand)
        {
            _owner = owner;
            _timerCommand = timerCommand;
            _remainingTime = duration;
        }

        public void Update(float dt)
        {
            _remainingTime -= dt;

            if (_remainingTime > 0) return;
            
            _timerCommand.Execute();
            _owner.Remove(this);
        }
    }
}