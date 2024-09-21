using System;
using UnityEngine;

namespace _Scripts
{
    public class AnalogClock : ICanChangeTime
    {
        private readonly MinuteHand _minuteHand;
        private readonly HourHand _hourHand;
        private readonly RectTransform _secondHand;

        public event Action<TimeData> onTimeChange;

        public AnalogClock(HourHand hourHand, MinuteHand minuteHand, RectTransform secondHand)
        {
            _hourHand = hourHand;
            _minuteHand = minuteHand;
            _secondHand = secondHand;

            _hourHand.onRotationChange += ChangeTime;
            _minuteHand.onRotationChange += ChangeTime;
        }
        
        public void SetTime(TimeData dateTime)
        {
            _hourHand.SetTime(dateTime);
            _minuteHand.SetTime(dateTime);
            
            float secondRotation = dateTime.seconds * 6f;
            _secondHand.localRotation = Quaternion.Euler(0, 0, -secondRotation);
        }
        
        public void StartChangeTime()
        {
            _hourHand.StartChangeTime();
            _minuteHand.StartChangeTime();
        }

        public void StopChangeTime()
        {
            _hourHand.StopChangeTime();
            _minuteHand.StopChangeTime();
        }

        private void ChangeTime()
        {
            var hours = _hourHand.GetTime();
            var minutes = _minuteHand.GetTime();
            TimeData timeData = new TimeData(hours, minutes, 0);
            onTimeChange?.Invoke(timeData);
        }
    }
}