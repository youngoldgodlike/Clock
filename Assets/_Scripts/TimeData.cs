namespace _Scripts
{
    public struct TimeData
    {
        public int hours { get; private set; }
        public int minutes { get; private set; }
        public int seconds { get; private set; }
        
        public TimeData(int hours, int minutes, int seconds)
        {
            this.hours = hours;
            this.minutes = minutes;
            this.seconds = seconds;
        }
    }
}