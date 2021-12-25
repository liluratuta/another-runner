using System.Collections.Generic;
using AnotherRunner.Model.Timers;
using AnotherRunner.Model.Timers.TimerChildren;

namespace AnotherRunner.Model.Simulations
{
    public class TimerSimulation : ISimulation, ITimer, ITimerChildOwner
    {
        private readonly LinkedList<ITimerChild> _timerChildren = new LinkedList<ITimerChild>();
        private readonly Stack<ITimerChild> _removableChildren = new Stack<ITimerChild>();

        public void Update(float dt)
        {
            foreach (var timerChild in _timerChildren)
            {
                timerChild.Update(dt);
            }

            while (_removableChildren.Count != 0)
            {
                _timerChildren.Remove(_removableChildren.Pop());
            }
        }

        public void Add(float duration, ITimerCommand timerCommand)
        {
            _timerChildren.AddFirst(new OneShotTimerChild(this, duration, timerCommand));
        }

        public void Repeat(float interval, ITimerCommand timerCommand)
        {
            _timerChildren.AddLast(new RepeatTimerChild(interval, timerCommand));
        }

        public void Remove(ITimerChild timerChild) => _removableChildren.Push(timerChild);
    }
}