

namespace VLBeats.Game.Logic.Gameplay
{
    public enum Judgement {
        Miss,
        Ok,
        Good,
        Perfect
    }
    public static class HitWindows
    {
        public const double PERFECT = 50;
        public const double GOOD = 100;
        public const double OK = 150;
        public static Judgement Judge(double absDelta)
        {
            if (absDelta <= PERFECT) return Judgement.Perfect;
            if (absDelta <= GOOD) return Judgement.Good;
            if (absDelta <= OK) return Judgement.Ok;
            return Judgement.Miss;
        }
    }
}
