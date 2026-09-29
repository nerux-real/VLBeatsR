using System.Collections.Generic;
using System.Globalization;
using System.IO;
using osu.Framework.Logging;

namespace VLBeats.Game.Logic.Gameplay
{
    public class Beatmap
    {
        public List<HitObject> HitObjects { get; } = new List<HitObject>();
        public static Beatmap Parse(IEnumerable<string> lines)
        {
            var beatmap = new Beatmap();
            foreach (string raw in lines) {
                string line = raw.Trim();
                if (line.Length == 0 || line.StartsWith("//")) continue;
                string[] parts = line.Split(' ', System.StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 4
                    || !double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out double time)
                    || !float.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out float x)
                    || !float.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out float y))
                {
                    Logger.Log($"Skipping bad beatmap line: {line}");
                    continue;
                }
                beatmap.HitObjects.Add(new HitObject(time, x, y, parts[3]));
            }
            beatmap.HitObjects.Sort((a, b) => a.Time.CompareTo(b.Time));
            return beatmap;
        }
        public static Beatmap LoadFromFile(string path) => Parse(File.ReadLines(path));
    }
}
