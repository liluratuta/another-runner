using System.Collections.Generic;
using System.Linq;

namespace AnotherRunner.Model.Players
{
    public class MultipliableValue
    {
        public float Value { get; private set; }
        
        private readonly float _originalValue;
        private readonly LinkedList<float> _multipliers = new LinkedList<float>();

        public MultipliableValue(float originalValue)
        {
            _originalValue = originalValue;
            Value = _originalValue;
        }

        public void AddMultiplier(float multiplier)
        {
            Value *= multiplier;
            _multipliers.AddLast(multiplier);
        }

        public void RemoveMultiplier(float multiplier)
        {
            var isSuccessRemove = _multipliers.Remove(multiplier);

            if (!isSuccessRemove)
            {
                return;
            }

            var commonMultiplier = _multipliers.Aggregate(1f, (current, m) => current * m);
            Value = _originalValue * commonMultiplier;
        }
    }
}