using System;

namespace Core
{
    public class Timer
    {
        protected float _timer;

        public event Action OnExpired;

        public virtual void SetTimer(float value)
        {
            _timer = value;
        }
        public virtual void Update(float delta)
        {
            _timer -= delta;

            if(_timer <= 0)
                OnExpired?.Invoke();
        }
    }
}