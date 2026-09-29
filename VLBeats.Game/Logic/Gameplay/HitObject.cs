namespace VLBeats.Game.Logic.Gameplay
{
    public class HitObject
    {
        public double Time { get; }
        public float X { get; }
        public float Y { get;  }
        public string Key { get;  }
        public HitObject(double time, float x, float y, string key)
        {
            Time = time;
            X = x;
            Y = y;
            Key = key;
        }
    }
}
