using System;

namespace JetPackJoyride.Scripts.Components {
    // max ignored if not randomized
    public class Timer(double value, double minResetValue, double maxResetValue, bool randomized) {
        private double _value = value;
        private readonly double _maxResetValue = maxResetValue;
        private readonly double _minResetValue = minResetValue;
        private readonly bool _randomized = randomized;
    
        // must be called every frame to keep the timer up-to-date
        // returns True if the Timer is firing
        public bool UpdateTimer(double delta) {
            _value -= delta;
            var active = _value <= 0.0f;
            if (active) {
                Reset();
            }
            return active;
        }

        private void Reset() {
            if (_randomized) {
                _value = Globals.GlobalRandom.Next((int)_minResetValue, (int)_maxResetValue);
            }
            else {
                _value = _minResetValue;
            }
        }
    }
}

