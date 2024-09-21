

using UnityEngine;

namespace _Scripts
{
    public class HourHand : TimeHand
    {
        public override int GetTime()
        {
            var rotationAngle = transform.localEulerAngles.z - 360;
            var hour = rotationAngle * 30 / 900 * -1;
            
            return (int)hour;
        }

        public override void SetTime(TimeData timeData)
        {
            float hourRotation = timeData.hours * 900 / 30 + timeData.minutes * 0.5f;
            transform.localRotation = Quaternion.Euler(0, 0, -hourRotation);
        }
    }
}