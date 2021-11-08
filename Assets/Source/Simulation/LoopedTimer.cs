namespace Core
{
    public class LoopedTimer : Timer
    {
        private float _duration;

        public int Loops { get; private set; }

        public LoopedTimer(float duration)
        {
            _duration = duration;
            OnExpired += () =>
            {
                _timer = _duration;
                Loops++;
            };
        }

        public override void SetTimer(float value)
        {
            base.SetTimer(value);
            _duration = value;
        }
        public void Reset()
        {
            _timer = _duration;
        }
    }
}