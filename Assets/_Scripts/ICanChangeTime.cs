using System;

namespace _Scripts
{
    public interface ICanChangeTime
    {
        public event Action<TimeData> onTimeChange;

        public void StartChangeTime();
        public void StopChangeTime();
    }
}