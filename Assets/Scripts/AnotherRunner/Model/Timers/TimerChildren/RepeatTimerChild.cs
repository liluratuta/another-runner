using UnityEngine;

namespace AnotherRunner.Model.Timers.TimerChildren
{
    public class RepeatTimerChild : ITimerChild
    {
        private readonly float _interval;
        private readonly ITimerCommand _timerCommand;
        private float _remainingTime;
        
        public RepeatTimerChild(float interval, ITimerCommand timerCommand)
        {
            _interval = interval;
            _timerCommand = timerCommand;

            _remainingTime = _interval;
        }

        public void Update(float dt)
        {
            _remainingTime -= dt;

            if (_remainingTime > 0) return;
            
            _timerCommand.Execute();
            
            var delta = Mathf.Abs(_remainingTime);
            _remainingTime = _interval - delta;
        }
    }
}