using UnityEngine;

namespace _Scripts
{
    public class MinuteHand : TimeHand
    {
        public override int GetTime()
        {
            var rotationAngle = transform.localEulerAngles.z - 360;
            var minute = rotationAngle * 0.5f / 3 * -1;
           
            return (int)minute;
        }

        public override void SetTime(TimeData timeData)
        {
            float minuteRotation = timeData.minutes * 3 / 0.5f;
            transform.localRotation = Quaternion.Euler(0, 0, -minuteRotation);
        }
    }
}